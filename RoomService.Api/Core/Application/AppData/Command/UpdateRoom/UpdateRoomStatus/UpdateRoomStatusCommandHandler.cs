using MediatR;
using Microsoft.EntityFrameworkCore;
using RoomService.Api.Core.Application.Interfaces;

namespace RoomService.Api.Core.Application.AppData.Command.UpdateRoom.UpdateRoomStatus
{
    public class UpdateRoomStatusCommandHandler : IRequestHandler<UpdateRoomStatusCommand>
    {
        private readonly IRoomDbContext _context;

        public UpdateRoomStatusCommandHandler(IRoomDbContext context)
        {
            _context = context;
        }

        async Task IRequestHandler<UpdateRoomStatusCommand>.Handle(UpdateRoomStatusCommand request, CancellationToken cancellationToken)
        {
            var resource = await _context.Rooms
                .FirstOrDefaultAsync(r => r.Id == request.RoomId, cancellationToken);

            if (resource == null)
            {
                throw new Exception("Ресурс не найден.");
            }

            resource.Status = request.Status;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
