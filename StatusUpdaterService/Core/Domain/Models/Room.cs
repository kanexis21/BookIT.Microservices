namespace StatusUpdaterService.Core.Domain.Models
{
    public class Room
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int Capacity { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public string Status { get; set; }
    }
}
