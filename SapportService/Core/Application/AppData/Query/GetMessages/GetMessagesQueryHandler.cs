using MediatR;
using Microsoft.EntityFrameworkCore;
using SapportService.Core.Domain.Models;
using SupportService.Core.Application.Interfaces;
using SupportService.Core.Domain.Models.Dto;

namespace SupportService.Core.Application.AppData.Query.GetMessageById
{
    public class GetMessagesQueryHandler : IRequestHandler<GetMessagesQuery, List<MessageDto>>
    {
        private readonly IMessageDbContext _context;
        private readonly Guid SupportUserId = Guid.Parse("тут-ид-техподдержки");

        public GetMessagesQueryHandler(IMessageDbContext context)
        {
            _context = context;
        }

        public async Task<List<MessageDto>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
        {
            var messages = await _context.Messages
                .Where(m =>
                    (m.SenderId == request.UserId && m.ReceiverId == SupportUserId) ||
                    (m.SenderId == SupportUserId && m.ReceiverId == request.UserId))
                .OrderBy(m => m.Timestamp)
                .Select(m => new MessageDto
                {
                    Id = m.Id,
                    SenderName = m.SenderName,
                    Text = m.Text,
                    Timestamp = m.Timestamp
                })
                .ToListAsync(cancellationToken);

            return messages;
        }
    }


}
