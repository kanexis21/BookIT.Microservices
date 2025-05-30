namespace StatusUpdaterService.Core.Domain.Models
{
    public class Booking
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public Guid ResourceId { get; set; }
        public Guid RoomId { get; set; }
        public DateTime EndTime { get; set; }
    }
}
