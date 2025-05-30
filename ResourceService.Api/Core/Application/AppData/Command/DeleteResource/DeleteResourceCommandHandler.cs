using MediatR;
using ResourceService.Api.Application.Interfaces;

namespace ResourceService.Api.Application.AppData.Command.DeleteResource
{
    internal class DeleteResourceCommandHandler : IRequestHandler<DeleteResourceCommand>
    {
        private readonly IResourceDbContext _context;

        public DeleteResourceCommandHandler(IResourceDbContext context)
        {
            _context = context;
        }
        public async Task Handle(DeleteResourceCommand request, CancellationToken cancellationToken)
        {
            var resource = await _context.Resources.FindAsync(new object[] { request.Id }, cancellationToken);

            if (resource == null)
                throw new KeyNotFoundException($"Resource with Id {request.Id} not found.");

            _context.Resources.Remove(resource);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
