namespace SupportService.Core.Domain.Models.Dto
{
    public class BookingViewModel
    {
        public Guid Id { get; set; }
        public Guid? ResourceId { get; set; }
        public Guid? RoomId { get; set; }
        public Guid? UserId { get; set; }
        public string UserName {  get; set; }
        public string? ResourceName { get; set; }
        public string? RoomName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
