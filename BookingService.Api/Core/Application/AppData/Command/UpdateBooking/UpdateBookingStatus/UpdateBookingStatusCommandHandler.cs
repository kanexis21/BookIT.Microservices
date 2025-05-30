using BookingService.Api.Core.Application.Interfaces;
using BookingService.Api.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Api.Application.AppData.Command.UpdateBooking.UpdateBookingStatus
{
    public class UpdateBookingStatusCommandHandler : IRequestHandler<UpdateBookingStatusCommand>
    {
        private readonly IBookingDbContext _context;

        public UpdateBookingStatusCommandHandler(IBookingDbContext context)
        {
            _context = context;
        }

        async Task IRequestHandler<UpdateBookingStatusCommand>.Handle(UpdateBookingStatusCommand request, CancellationToken cancellationToken)
        {
            var booking = await _context.Bookings
                .FirstOrDefaultAsync(x => x.Id == request.BookingId, cancellationToken);

            if (booking == null)
                throw new Exception("Не найдено бронирование");

            booking.Status = request.Status;

            await _context.SaveChangesAsync(cancellationToken);

        }
    }

}
