using MediatR;
using SupportService.Core.Domain.Models.Dto;

namespace SupportService.Core.Application.AppData.Query.GetAllMesagges
{
    public class GetMessagesQuery : IRequest<List<MessageDto>>
    {
        public Guid User1 { get; set; }
        public Guid User2 { get; set; }
    }
}
