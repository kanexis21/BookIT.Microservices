using BookingService.Api.Core.Application.Interfaces;
using BookingService.Api.Core.Domain.Model;
using BookingService.Api.Infrastructure;
using MediatR;

namespace BookingService.Api.Application.AppData.Command.UpdateBooking
{
    public class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand, bool>
    {
        private readonly IBookingDbContext _context;

        public UpdateBookingCommandHandler(IBookingDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = await _context.Bookings.FindAsync(new object[] { request.Id }, cancellationToken);

            if (booking == null)
                return false;

            booking.ResourceId = request.ResourceId;
            booking.RoomId = request.RoomId;
            booking.UserId = request.UserId;
            booking.StartTime = request.StartTime;
            booking.EndTime = request.EndTime;
            booking.Description = request.Description;

            if (!string.IsNullOrEmpty(request.Status) &&
                Enum.TryParse<BookingStatus>(request.Status, out var status))
            {
                booking.Status = status;
            }

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

}
