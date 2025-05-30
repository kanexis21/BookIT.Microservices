using MediatR;
using ResourceService.Api.Application.Interfaces;

namespace ResourceService.Api.Application.AppData.Command.UpdateResource
{
    internal class UpdateResourceCommandHandler : IRequestHandler<UpdateResourceCommand>
    {
        private readonly IResourceDbContext _context;

        public UpdateResourceCommandHandler(IResourceDbContext context)
        {
            _context = context;
        }
        public async Task Handle(UpdateResourceCommand request, CancellationToken cancellationToken)
        {
            var resource = await _context.Resources.FindAsync(new object[] { request.Id }, cancellationToken);

            if (resource == null)
                throw new KeyNotFoundException($"Ресурс с Id {request.Id} не найден.");

            resource.Name = request.Name;
            resource.Location = request.Location;
            resource.Description = request.Description;
            resource.Status = request.Status;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
