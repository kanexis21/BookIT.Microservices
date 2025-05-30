using BookingService.Api.Core.Domain.Model;
using MediatR;

namespace BookingService.Api.Application.AppData.Queries.GetAllBooking
{
    public class GetAllBookingQuery : IRequest<List<Booking>>
    {
    }
}
