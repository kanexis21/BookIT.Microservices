namespace RoomService.Api.Core.Domain.Model
{
    public enum RoomStatus
    {
        Свободно,
        Забронировано
    }
    public class Room
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int Capacity { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public RoomStatus Status { get; set; }
        public List<RoomPhoto> Photos { get; set; } = new();
    }
}
