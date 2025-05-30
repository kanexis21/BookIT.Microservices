using BookingService.Api.Core.Application.Interfaces;
using BookingService.Api.Core.Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Api.Application.AppData.Queries.GetAllBooking
{
    public class GetAllBookingsQueryHandler : IRequestHandler<GetAllBookingQuery, List<Booking>>
    {
        private readonly IBookingDbContext _context;

        public GetAllBookingsQueryHandler(IBookingDbContext context)
        {
            _context = context;
        }

        public async Task<List<Booking>> Handle(GetAllBookingQuery request, CancellationToken cancellationToken)
        {
            return await _context.Bookings.ToListAsync(cancellationToken);
        }
    }
}
