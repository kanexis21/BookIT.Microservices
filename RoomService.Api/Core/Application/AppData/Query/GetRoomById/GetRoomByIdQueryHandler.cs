using MediatR;
using Microsoft.EntityFrameworkCore;
using RoomService.Api.Core.Application.Interfaces;
using RoomService.Api.Core.Domain.Model;

namespace RoomService.Api.Core.Application.AppData.Query.GetRoomById
{
    public class GetRoomByIdQueryHandler : IRequestHandler<GetRoomByIdQuery, Room?>
    {
        private readonly IRoomDbContext _context;

        public GetRoomByIdQueryHandler(IRoomDbContext context)
        {
            _context = context;
        }

        public async Task<Room?> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Rooms
                .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);
        }
    }
}
