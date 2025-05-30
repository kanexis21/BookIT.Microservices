using MediatR;
using SapportService.Core.Domain.Models;

namespace SupportService.Core.Application.AppData.Command.CreateMessage
{
    public class CreateMessageCommand : IRequest<Guid>
    {
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public string Text { get; set; } = string.Empty;
    }


}
