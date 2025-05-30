using MediatR;
using ResourceService.Api.Application.Interfaces;
using ResourceService.Api.Core.Domain.Model;

namespace ResourceService.Api.Application.AppData.Command.CreateResource
{
    public class CreateResourceCommandHandler : IRequestHandler<CreateResourceCommand, Guid>
    {
        private readonly IResourceDbContext _context;

        public CreateResourceCommandHandler(IResourceDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateResourceCommand request, CancellationToken cancellationToken)
        {
            var resource = new Resource
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Location = request.Location
            };

            _context.Resources.Add(resource);
            await _context.SaveChangesAsync(cancellationToken);
            return resource.Id;
        }
    }
}
