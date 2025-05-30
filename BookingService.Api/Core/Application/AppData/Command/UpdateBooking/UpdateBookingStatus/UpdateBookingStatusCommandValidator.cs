using FluentValidation;

namespace BookingService.Api.Application.AppData.Command.UpdateBooking.UpdateBookingStatus
{
    public class UpdateBookingStatusCommandValidator : AbstractValidator<UpdateBookingStatusCommand>
    {
        public UpdateBookingStatusCommandValidator()
        {
            RuleFor(x => x.BookingId).NotEmpty();
            RuleFor(x => x.Status).IsInEnum();
        }
    }

}
