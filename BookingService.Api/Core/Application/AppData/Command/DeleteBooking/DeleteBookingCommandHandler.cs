using BookingService.Api.Core.Application.Interfaces;
using BookingService.Api.Infrastructure;
using MediatR;

namespace BookingService.Api.Application.AppData.Command.DeleteBooking
{
    public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand, bool>
    {
        private readonly IBookingDbContext _context;

        public DeleteBookingCommandHandler(IBookingDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = await _context.Bookings.FindAsync(new object[] { request.Id }, cancellationToken);

            if (booking == null)
                return false;

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }

}
