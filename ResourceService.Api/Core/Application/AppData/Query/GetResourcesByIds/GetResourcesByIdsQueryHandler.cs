using MediatR;
using Microsoft.EntityFrameworkCore;
using ResourceService.Api.Application.Interfaces;
using ResourceService.Api.Core.Domain.Model;

namespace ResourceService.Api.Core.Application.AppData.Query.GetResourcesByIds
{
    public class GetResourcesByIdsQueryHandler : IRequestHandler<GetResourcesByIdsQuery, List<ResourceShortViewModel>>
    {
        private readonly IResourceDbContext _context;

        public GetResourcesByIdsQueryHandler(IResourceDbContext context)
        {
            _context = context;
        }

        public async Task<List<ResourceShortViewModel>> Handle(GetResourcesByIdsQuery request, CancellationToken cancellationToken)
        {
            var resources = await _context.Resources
                .Where(r => request.Ids.Contains(r.Id))
                .Select(r => new ResourceShortViewModel
                {
                    Id = r.Id,
                    Name = r.Name
                })
                .ToListAsync(cancellationToken);

            return resources;
        }
    }

}
