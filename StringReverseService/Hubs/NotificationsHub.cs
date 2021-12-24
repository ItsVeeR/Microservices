using Microsoft.AspNet.SignalR;
using System;

namespace StringReverseService.Hubs
{
    public class NotificationsHub : Hub
    {
        private static IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationsHub>();

        public void NotifyAllClients()
        {
            Clients.All.NotifyAllClients();
        }

        // This can be used to notify selective clients based on prior Condition.
        public static void NotifySelectedClients()
        {
            throw new NotImplementedException();
        }
    }
}
