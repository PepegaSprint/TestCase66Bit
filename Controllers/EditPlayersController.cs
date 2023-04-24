using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCase66bit.Models;
using TestCase66Bit.Data;
using TestCase66Bit.Hubs;


namespace TestCase66Bit.Controllers
{
    public class EditPlayersController : Controller
    {
        private readonly IHubContext<DbHub> _hubContext;
        private readonly ApplicationDbContext _db;
        public EditPlayersController(ApplicationDbContext db, IHubContext<DbHub> hubContext)
        {
            _hubContext = hubContext;
            _db = db;
        }


        public IActionResult Index()
        {
            IEnumerable<FootballPlayerModel> objList = _db.FootballPlayers;
            ViewBag.Countries = new List<string> { "Россия", "США", "Италия" };
            ViewBag.Teams = _db.FootballPlayers.Select(m => m.team).Distinct().ToList();
            return View(objList);
        }

        
        public async Task<IActionResult> Edit(int id, string lastName, string firstName, string birthDate, 
            string gender, string team, string country)  
        {
            Console.WriteLine(birthDate);
            if (id == 0)
            return NotFound();
        
        var obj = _db.FootballPlayers.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Update(obj);
                obj.firstName = firstName;
                obj.lastName = lastName;
                obj.gender = gender;
                obj.birthDate = Convert.ToDateTime(birthDate);
                obj.team = team;
                obj.country = country;
                await _db.SaveChangesAsync();
                await _hubContext.Clients.All.SendAsync("PlayerEdited", obj);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        
        public async Task<IActionResult> Delete(int id)
        {

            var obj = _db.FootballPlayers.Find(id);
            if (obj == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                _db.FootballPlayers.Remove(obj);
                _db.SaveChanges();
                await _hubContext.Clients.All.SendAsync("PlayerDeleted", obj);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
    }
}
