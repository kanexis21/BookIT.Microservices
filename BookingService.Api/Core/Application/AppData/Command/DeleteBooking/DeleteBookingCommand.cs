using MediatR;

namespace BookingService.Api.Application.AppData.Command.DeleteBooking
{
    public record DeleteBookingCommand(Guid Id) : IRequest<bool>;

}
