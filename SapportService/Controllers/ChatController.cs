using MediatR;
using Microsoft.AspNetCore.Mvc;
using SupportService.Core.Application.AppData.Command.CreateMessage;
using SupportService.Core.Application.AppData.Query.GetAllMesagges;

namespace SupportService.Controllers
{
    [ApiController]
    [Route("api/chat")]
    public class ChatController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChatController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage(CreateMessageCommand command)
        {
            var messageId = await _mediator.Send(command);
            return Ok(messageId);
        }

        [HttpGet("messages")]
        public async Task<IActionResult> GetMessages(Guid user1, Guid user2)
        {
            var messages = await _mediator.Send(new GetMessagesQuery { User1 = user1, User2 = user2 });
            return Ok(messages);
        }

    }
}
