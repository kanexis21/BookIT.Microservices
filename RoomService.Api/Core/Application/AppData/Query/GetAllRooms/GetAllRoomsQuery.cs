using MediatR;
using RoomService.Api.Core.Domain.Model;

namespace RoomService.Api.Core.Application.AppData.Queries.GetAllRooms
{
    public class GetAllRoomsQuery : IRequest<List<Room>> { }

}
