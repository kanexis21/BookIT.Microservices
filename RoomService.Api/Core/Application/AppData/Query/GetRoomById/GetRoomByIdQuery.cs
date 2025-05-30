using MediatR;
using RoomService.Api.Core.Domain.Model;

namespace RoomService.Api.Core.Application.AppData.Query.GetRoomById
{
    public record GetRoomByIdQuery(Guid Id) : IRequest<Room>
    {
    }
}
