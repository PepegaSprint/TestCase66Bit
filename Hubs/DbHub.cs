using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace TestCase66Bit.Hubs
{
    public class DbHub : Hub
    {
        public Task SendMessage(string message)
        {
            return Clients.All.SendAsync("Send", message);
        }

    }
}
