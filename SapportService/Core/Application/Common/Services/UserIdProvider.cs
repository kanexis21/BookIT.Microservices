using Microsoft.AspNetCore.SignalR;

namespace SupportService.Core.Application.Common.Services
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public string? GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst("sub")?.Value
                   ?? connection.User?.Identity?.Name;
        }
    }

}
