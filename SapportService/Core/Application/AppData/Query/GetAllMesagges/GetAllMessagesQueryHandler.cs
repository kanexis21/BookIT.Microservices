using MediatR;
using Microsoft.EntityFrameworkCore;
using SapportService.Core.Domain.Models;
using SupportService.Core.Application.Interfaces;
using SupportService.Core.Domain.Models.Dto;

namespace SupportService.Core.Application.AppData.Query.GetAllMesagges
{
    public class GetMessagesQueryHandler : IRequestHandler<GetMessagesQuery, List<MessageDto>>
    {
        private readonly IMessageDbContext _dbContext;

        public GetMessagesQueryHandler(IMessageDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<MessageDto>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
        {
            var messages = await _dbContext.Messages
                .Where(m =>
                    (m.SenderId == request.User1 && m.ReceiverId == request.User2) ||
                    (m.SenderId == request.User2 && m.ReceiverId == request.User1))
                .OrderBy(m => m.Timestamp)
                .ToListAsync(cancellationToken);

            return messages.Select(m => new MessageDto
            {
                Text = m.Text,
                Timestamp = m.Timestamp,
                SenderName = m.SenderName,
                IsOwnMessage = m.SenderId == request.User1
            }).ToList();
        }
    }


}
