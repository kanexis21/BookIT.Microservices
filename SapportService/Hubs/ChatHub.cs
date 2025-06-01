using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace SapportService.Hubs
{
    [Authorize]
    public class SupportHub : Hub
    {
        private static readonly Dictionary<string, string> UserConnections = new();

        public override Task OnConnectedAsync()
        {
            foreach (var claim in Context.User.Claims)
            {
                Console.WriteLine($"CLAIM: {claim.Type} = {claim.Value}");
            }
            if (Context.User == null)
            {
                Console.WriteLine("Context.User == null");
                return base.OnConnectedAsync();
            }

            var userId = Context.UserIdentifier;

            if (string.IsNullOrEmpty(userId))
            {
                Console.WriteLine("Невозможно зарегистрироваться userId - null или пуст");
                return base.OnConnectedAsync();
            }

            UserConnections[userId] = Context.ConnectionId;
            return base.OnConnectedAsync();
        }


        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.UserIdentifier;

            if (!string.IsNullOrEmpty(userId))
                UserConnections.Remove(userId);

            return base.OnDisconnectedAsync(exception);
        }


        public async Task SendMessage(string fromUserId, string toUserId, string message)
        {

            if (UserConnections.TryGetValue(toUserId, out var connectionId))
            {
                var payload = new
                {
                    senderId = fromUserId,
                    text = message,
                    senderName = "Имя или логин отправителя"
                };
                await Clients.Client(connectionId).SendAsync("ReceiveMessage", payload);

            }

        }
    }

}
