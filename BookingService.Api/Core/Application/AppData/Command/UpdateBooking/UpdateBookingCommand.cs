using MediatR;

namespace BookingService.Api.Application.AppData.Command.UpdateBooking
{
    public class UpdateBookingCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public Guid? ResourceId { get; set; }
        public Guid? RoomId { get; set; }
        public Guid UserId { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public string? Description { get; set; }
        public string? Status { get; set; }
    }

}
