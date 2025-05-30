using FluentValidation;

namespace BookingService.Api.Core.Application.AppData.Queries.GetMyBookings
{
    public class GetMyBookingsQueryValidator : AbstractValidator<GetMyBookingsQuery>
    {
        public GetMyBookingsQueryValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId обязателен");
        }
    }

}
