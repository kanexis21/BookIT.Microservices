using MediatR;
using ResourceService.Api.Application.Interfaces;
using ResourceService.Api.Core.Domain.Model;

namespace ResourceService.Api.Application.AppData.Queries.GetResourceById
{
    public class GetResourceByIdQueryHandler : IRequestHandler<GetResourceByIdQuery, Resource?>
    {
        private readonly IResourceDbContext _context;

        public GetResourceByIdQueryHandler(IResourceDbContext context)
        {
            _context = context;
        }

        public async Task<Resource?> Handle(GetResourceByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Resources.FindAsync(new object[] { request.Id }, cancellationToken);
        }
    }
}
