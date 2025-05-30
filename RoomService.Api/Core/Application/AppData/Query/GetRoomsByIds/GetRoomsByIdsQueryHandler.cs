using MediatR;
using Microsoft.EntityFrameworkCore;
using RoomService.Api.Core.Application.Interfaces;
using RoomService.Api.Core.Domain.Model;


namespace RoomService.Api.Core.Application.AppData.Query.GetResourcesByIds
{
    public class GetRoomsByIdsQueryHandler : IRequestHandler<GetRoomsByIdsQuery, List<RoomShortViewModel>>
    {
        private readonly IRoomDbContext _context;

        public GetRoomsByIdsQueryHandler(IRoomDbContext context)
        {
            _context = context;
        }

        public async Task<List<RoomShortViewModel>> Handle(GetRoomsByIdsQuery request, CancellationToken cancellationToken)
        {
            var resources = await _context.Rooms
                .Where(r => request.Ids.Contains(r.Id))
                .Select(r => new RoomShortViewModel
                {
                    Id = r.Id,
                    Name = r.Name
                })
                .ToListAsync(cancellationToken);

            return resources;
        }
    }

}
