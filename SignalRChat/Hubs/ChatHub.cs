using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        public async Task JoinChat(string user)
        {
            await Clients.All.SendAsync("JoinChat", user);
        }

        public async Task LeaveChat(string user)
        {
            await Clients.All.SendAsync("LeaveChat", user);
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        //JOIN GROUP
        public async Task JoinGroupChat(string group, string user)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, group);
            await Clients.Group(group).SendAsync("JoinGroupMessage", group, user);
        }

        public async Task JoinGroupMessage(string group, string user)
        {
            await Clients.Group(group).SendAsync("JoinGroupMessage", user);
        }

        //LEAVE GROUP
        public async Task LeaveGroupChat(string group, string user)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, group);
            await Clients.Group(group).SendAsync("LeaveGroupMessage", group, user);
        }
        public async Task LeaveGroupMessage(string group, string user)
        {
            await Clients.Group(group).SendAsync("LeaveGroupMessage", user);
        }

        //SEND GROUP MESSAGE
        public async Task SendGroupMessage(string group, string user, string message)
        {
            await Clients.Group(group).SendAsync("ReceiveGroupMessage", user, message);
        }

      
       
    }
}
