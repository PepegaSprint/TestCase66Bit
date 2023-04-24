using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TestCase66Bit.Models;
using TestCase66bit.Models;

using TestCase66Bit.Data;
using Microsoft.AspNetCore.SignalR;
using TestCase66Bit.Hubs;

namespace TestCase66Bit.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHubContext<DbHub> _hubContext;
        private readonly ApplicationDbContext _db;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db, IHubContext<DbHub> hubContext)
        {
            _db = db;
            _logger = logger;
            _hubContext = hubContext;
        }


        public IActionResult Index()
        {
            ViewBag.Countries = new List<string> {"Россия", "США", "Италия"};
            ViewBag.Teams = _db.FootballPlayers.Select(m => m.team).Distinct().ToList();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FootballPlayerModel obj)
        {
            if (ModelState.IsValid)
            {
                _db.FootballPlayers.Add(obj);
                _db.SaveChanges();
                await _hubContext.Clients.All.SendAsync("NewPlayerCreated",obj);
                return RedirectToAction("Index");
            }
            else
            {
               return View(obj);
            }
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
