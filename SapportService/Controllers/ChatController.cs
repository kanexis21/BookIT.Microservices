using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SapportService.Core.Domain.Models;
using SapportService.Hubs;
using SupportService.Core.Application.AppData.Command.CreateMessage;
using SupportService.Core.Application.AppData.Query.GetMessageById;
using SupportService.Core.Application.AppData.Query.GetUserChats;

namespace SupportService.Controllers
{
    [ApiController]
    [Route("api/chat")]
    public class ChatController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHubContext<SupportHub> _hubContext;
        public ChatController(IMediator mediator, IHubContext<SupportHub> hubContext)
        {
            _mediator = mediator;
            _hubContext = hubContext;
        }

        [HttpPost("messages/send")]
        public async Task<IActionResult> SendMessage(CreateMessageCommand command)
        {
            var messageId = await _mediator.Send(command);

            var messagePayload = new
            {
                senderId = command.SenderId,
                senderName = command.SenderName,
                content = command.Text,
                receiverId = command.ReceiverId,
                messageId = messageId
            };

            await _hubContext.Clients.User(command.ReceiverId.ToString())
                .SendAsync("ReceiveMessage", messagePayload);

            return Ok(messageId);
        }

        [HttpGet("chats")]
        public async Task<IActionResult> GetUserChats()
        {
            var users = await _mediator.Send(new GetUserChatsQuery());
            return Ok(users);
        }

        [HttpGet("messages/{userId}")]
        public async Task<IActionResult> GetMessages(Guid userId)
        {
            var messages = await _mediator.Send(new GetMessagesQuery(userId));
            return Ok(messages);
        }

    }
}
