using MediatR;

namespace SupportService.Core.Application.AppData.Command.DeleteMessage
{
    public class DeleteMessageCommand : IRequest
    {
        public Guid Id { get; set; }
    }

}
