using BookingService.Api.Core.Domain.Model;
using MediatR;

namespace BookingService.Api.Application.AppData.Queries.GetBookingById
{
    public record GetBookingByIdQuery(Guid Id) : IRequest<Booking?>;
}
