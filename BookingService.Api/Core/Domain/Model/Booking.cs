namespace BookingService.Api.Core.Domain.Model
{
    public enum BookingStatus
    {
        Забронировано,
        Недоступно,
        Истёк
    }
    public class Booking
    {
        public Guid Id { get; set; }
        public Guid? ResourceId { get; set; }
        public Guid? RoomId { get; set; }
        public Guid UserId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public BookingStatus Status { get; set; }
    }
}
