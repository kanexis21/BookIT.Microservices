using BookingService.Api.Core.Domain.Model;
using MediatR;

namespace BookingService.Api.Core.Application.AppData.Queries.GetMyBookings
{
    public class GetMyBookingsQuery : IRequest<List<Booking>>
    {
        public Guid UserId { get; set; }
    }

}
