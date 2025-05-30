using MediatR;
using RoomService.Api.Core.Domain.Model;
using System.Text.Json.Serialization;

namespace RoomService.Api.Core.Application.AppData.Command.UpdateRoom.UpdateRoomStatus
{
    public class UpdateRoomStatusCommand : IRequest
    {
        public Guid RoomId { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public RoomStatus Status { get; set; }
    }
}
