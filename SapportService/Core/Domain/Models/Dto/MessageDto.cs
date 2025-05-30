namespace SupportService.Core.Domain.Models.Dto
{
    public class MessageDto
    {
        public string Text { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public string SenderName { get; set; } = string.Empty;
        public bool IsOwnMessage { get; set; }
    }

}
