using MediatR;
using Microsoft.EntityFrameworkCore;
using RoomService.Api.Core.Application.Interfaces;
using RoomService.Api.Core.Domain.Model;
using RoomService.Api.Infrastructure;

namespace RoomService.Api.Core.Application.AppData.Queries.GetAllRooms
{
    public class GetAllRoomsQueryHandler : IRequestHandler<GetAllRoomsQuery, List<Room>>
    {
        private readonly IRoomDbContext _context;
        public GetAllRoomsQueryHandler(IRoomDbContext context) => _context = context;

        public async Task<List<Room>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Rooms.ToListAsync(cancellationToken);
        }
    }

}
