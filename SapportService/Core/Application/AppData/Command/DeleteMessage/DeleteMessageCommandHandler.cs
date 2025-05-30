using MediatR;
using SupportService.Core.Application.Interfaces;

namespace SupportService.Core.Application.AppData.Command.DeleteMessage
{
    public class DeleteMessageCommandHandler : IRequestHandler<DeleteMessageCommand>
    {
        private readonly IMessageDbContext _context;

        public DeleteMessageCommandHandler(IMessageDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Messages.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity is null)
                return;

            _context.Messages.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

        }
    }

}
