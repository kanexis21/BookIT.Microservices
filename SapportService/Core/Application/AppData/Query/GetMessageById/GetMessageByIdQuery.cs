using MediatR;
using SapportService.Core.Domain.Models;

namespace SupportService.Core.Application.AppData.Query.GetMessageById
{
    public class GetMessageByIdQuery : IRequest<Message?>
    {
        public Guid Id { get; set; }
    }

}
