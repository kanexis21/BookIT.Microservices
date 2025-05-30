namespace BookingService.Api.Core.Domain.Model.ViewModel
{
    public class CreateBookingRequest
    {
        public Guid? RoomId { get; set; }
        public Guid UserId { get; set; }
        public Guid? ResourceId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Description { get; set; }

    }
}
