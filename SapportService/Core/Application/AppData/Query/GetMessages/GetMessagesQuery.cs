using MediatR;
using SapportService.Core.Domain.Models;
using SupportService.Core.Domain.Models.Dto;

namespace SupportService.Core.Application.AppData.Query.GetMessageById
{
    public record GetMessagesQuery(Guid UserId) : IRequest<List<MessageDto>>;


}
