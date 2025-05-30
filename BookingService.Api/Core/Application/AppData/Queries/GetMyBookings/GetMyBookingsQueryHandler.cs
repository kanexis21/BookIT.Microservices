using BookingService.Api.Core.Domain.Model;
using BookingService.Api.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Api.Core.Application.AppData.Queries.GetMyBookings
{
    public class GetMyBookingsQueryHandler : IRequestHandler<GetMyBookingsQuery, List<Booking>>
    {
        private readonly BookingDbContext _context;

        public GetMyBookingsQueryHandler(BookingDbContext context)
        {
            _context = context;
        }

        public async Task<List<Booking>> Handle(GetMyBookingsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Bookings
                .Where(b => b.UserId == request.UserId)
                .Select(b => new Booking
                {
                    Id = b.Id,
                    ResourceId = b.ResourceId,
                    RoomId = b.RoomId,
                    StartTime = b.StartTime,
                    EndTime = b.EndTime,
                    Status = b.Status,
                    Description = b.Description
                })
                .ToListAsync(cancellationToken);
        }
    }

}
