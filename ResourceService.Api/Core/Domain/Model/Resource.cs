namespace ResourceService.Api.Core.Domain.Model
{
    public enum ResourceStatus
    {
        Доступен,
        Недоступен
    }
    public class Resource
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Location { get; set; }
        public string? Description { get; set; }

        public ResourceStatus Status { get; set; }

    }
}
