using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebFramework.SignalR.Hubs
{
    public class GroupHub : Hub
    {
        private static Dictionary<string, string> connectionsNgroup = new Dictionary<string, string>();

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            await Clients.All.SendAsync("HubConnected");
        }
        /// <summary>
        /// on disconnect
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            if (connectionsNgroup.ContainsKey(Context.ConnectionId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, connectionsNgroup[Context.ConnectionId]);
                connectionsNgroup.Remove(Context.ConnectionId);
            }
            await base.OnDisconnectedAsync(exception);
        }

        /// <summary>
        /// send text to clients who have the same group name
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public async Task BroadcastText(string text)
        {
            ///Context.ConnectionId : connection id
            ///connectionsNgroup[Context.ConnectionId] group name
            ///
            // check connection id is in current system
            if (connectionsNgroup.ContainsKey(Context.ConnectionId))
            {
                await Clients.OthersInGroup(connectionsNgroup[Context.ConnectionId]).SendAsync("ReceiveText", text);
            }
        }

        public async Task JoinGroup(string group)
        {
            if (!connectionsNgroup.ContainsKey(Context.ConnectionId))
            {
                connectionsNgroup.Add(Context.ConnectionId, group);
                await Groups.AddToGroupAsync(Context.ConnectionId, group);
            }
            
        }
    }
}
