using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace SapportService.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        public async Task SendMessage(string chatId, string userId, string userName, string message, string role)
        {
            var payload = new
            {
                ChatId = chatId,
                UserId = userId,
                UserName = userName,
                Message = message,
                Role = role,
                Timestamp = DateTime.UtcNow
            };

            await Clients.Group(chatId).SendAsync("ReceiveMessage", payload);
        }

        public override async Task OnConnectedAsync()
        {
            var chatId = Context.GetHttpContext()?.Request.Query["chatId"];
            if (!string.IsNullOrEmpty(chatId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
            }

            await base.OnConnectedAsync();
        }
    }
}
