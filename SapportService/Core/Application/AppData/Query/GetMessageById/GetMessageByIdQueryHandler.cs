using MediatR;
using SapportService.Core.Domain.Models;
using SupportService.Core.Application.Interfaces;

namespace SupportService.Core.Application.AppData.Query.GetMessageById
{
    public class GetMessageByIdQueryHandler : IRequestHandler<GetMessageByIdQuery, Message?>
    {
        private readonly IMessageDbContext _context;

        public GetMessageByIdQueryHandler(IMessageDbContext context)
        {
            _context = context;
        }

        public async Task<Message?> Handle(GetMessageByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Messages.FindAsync(new object[] { request.Id }, cancellationToken);
        }
    }

}
