using MediatR;
using RoomService.Api.Core.Domain.Model;

namespace RoomService.Api.Core.Application.AppData.Command.CreateRoom
{
    public class CreateRoomCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public List<RoomPhoto> Photos { get; set; } = new();
        public RoomStatus Status { get; set; }

    }

}
