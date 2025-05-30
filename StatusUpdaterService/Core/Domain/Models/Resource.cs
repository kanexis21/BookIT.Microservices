namespace StatusUpdaterService.Core.Domain.Models
{
    public class Resource
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
    }
}
