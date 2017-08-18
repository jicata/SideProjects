using Microsoft.AspNet.SignalR;

namespace ChatR
{
    using System.Threading.Tasks;
    using Microsoft.AspNet.SignalR.Hubs;

    [HubName("chat")]
    public class ChatHub : Hub
    {
        public void SendMessage(string message)
        {
            var msg = $"{Context.ConnectionId}: {message}";
            Clients.All.newMessage(msg);
        }

        public void JoinRoom(string room)
        {
            // NOTE: this is not persisted
            Groups.Add(Context.ConnectionId, room);
        }

        public void SendMessageToRoom(string room, string message)
        {
            var msg = $"{Context.ConnectionId}: {message}";
            Clients.Group(room).newMessage(msg);
        }

        public void SendMessageData(SendData data)
        {
            // process incodming data
            // transform data
            // craft new data

            Clients.All.newData(data);
        }

        public override Task OnConnected()
        {
            SendMonitoringData("Connected", Context.ConnectionId);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            SendMonitoringData("Disconnected", Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            SendMonitoringData("Reconnected", Context.ConnectionId);
            return base.OnReconnected();
        }

        private void SendMonitoringData(string eventType, string connection)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<MonitorHub>();
            context.Clients.All.newEvent(eventType, connection);
        }
    }

    public class SendData
    {
        public int Id { get; set; }

        public string Data { get; set; }
    }
}