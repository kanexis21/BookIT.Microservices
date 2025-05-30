using MediatR;
using Microsoft.EntityFrameworkCore;
using ResourceService.Api.Application.Interfaces;
using ResourceService.Api.Core.Domain.Model;

namespace ResourceService.Api.Application.AppData.Queries.GetAllResources
{
    public class GetAllResourcesQueryHandler : IRequestHandler<GetAllResourcesQuery, List<Resource>>
    {
        private readonly IResourceDbContext _context;

        public GetAllResourcesQueryHandler(IResourceDbContext context)
        {
            _context = context;
        }

        public async Task<List<Resource>> Handle(GetAllResourcesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Resources.ToListAsync(cancellationToken);
        }
    }

}
