namespace RoomService.Api.Core.Domain.Model
{
    public class RoomPhoto
    {
        public Guid Id { get; set; }
        public string FileName { get; set; } = null!;
        public Guid RoomId { get; set; }
        public Room Room { get; set; } = null!;
    }

}
