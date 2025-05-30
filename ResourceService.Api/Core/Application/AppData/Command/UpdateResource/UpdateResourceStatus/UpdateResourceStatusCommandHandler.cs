using MediatR;
using Microsoft.EntityFrameworkCore;
using ResourceService.Api.Application.Interfaces;


namespace ResourceService.Api.Application.AppData.Command.UpdateResource.UpdateResourceStatus
{
    public class UpdateResourceStatusCommandHandler : IRequestHandler<UpdateResourceStatusCommand>
    {
        private readonly IResourceDbContext _context;

        public UpdateResourceStatusCommandHandler(IResourceDbContext context)
        {
            _context = context;
        }

        async Task IRequestHandler<UpdateResourceStatusCommand>.Handle(UpdateResourceStatusCommand request, CancellationToken cancellationToken)
        {
            var resource = await _context.Resources
                .FirstOrDefaultAsync(r => r.Id == request.ResourceId, cancellationToken);

            if (resource == null)
            {
                throw new Exception("Ресурс не найден.");
            }

            resource.Status = request.Status;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }

}
