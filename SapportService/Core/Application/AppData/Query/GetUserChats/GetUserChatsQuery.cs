using MediatR;
using SupportService.Core.Domain.Models.Dto;

namespace SupportService.Core.Application.AppData.Query.GetUserChats
{
    public record GetUserChatsQuery() : IRequest<List<UserDto>>;
}
