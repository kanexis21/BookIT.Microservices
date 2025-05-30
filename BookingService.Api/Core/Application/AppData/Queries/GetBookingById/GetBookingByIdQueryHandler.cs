using BookingService.Api.Core.Application.Interfaces;
using BookingService.Api.Core.Domain.Model;
using BookingService.Api.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Api.Application.AppData.Queries.GetBookingById
{
    public class GetBookingByIdQueryHandler : IRequestHandler<GetBookingByIdQuery, Booking?>
    {
        private readonly IBookingDbContext _context;

        public GetBookingByIdQueryHandler(IBookingDbContext context)
        {
            _context = context;
        }

        public async Task<Booking?> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Bookings
                .Include(b => b.ResourceId)
                .Include(b => b.RoomId)
                .Include(b => b.UserId)
                .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);
        }
    }
}
