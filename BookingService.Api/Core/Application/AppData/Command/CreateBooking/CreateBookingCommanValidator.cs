using BookingService.Api.Core.Domain.Model;
using FluentValidation;

namespace BookingService.Api.Application.AppData.Command.CreateBooking
{
    public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
    {
        public CreateBookingCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Пользователь обязателен.");

            RuleFor(x => x)
                .Must(x => x.ResourceId != null || x.RoomId != null)
                .WithMessage("Укажите хотя бы комнату или ресурс.");

            RuleFor(x => x.StartTime)
                .NotEmpty()
                .LessThan(x => x.EndTime)
                .WithMessage("Время начала должно быть раньше времени окончания.");

            RuleFor(x => x.EndTime)
                .NotEmpty()
                .GreaterThan(x => x.StartTime)
                .WithMessage("Время окончания должно быть позже начала.");


            RuleFor(x => x.Description)
                .MaximumLength(500)
                .WithMessage("Описание не должно превышать 500 символов.");

            RuleFor(x => x.StartTime.TimeOfDay)
                .GreaterThanOrEqualTo(TimeSpan.FromHours(11))
                .WithMessage("Нельзя бронировать до 11:00");

            RuleFor(x => x.EndTime.TimeOfDay)
                .LessThanOrEqualTo(TimeSpan.FromHours(21))
                .WithMessage("Нельзя бронировать после 21:00");

            RuleFor(x => x.EndTime)
                .GreaterThan(x => x.StartTime)
                .WithMessage("Время окончания должно быть позже времени начала");

            RuleFor(x => x.Status)
                .Must(status => string.IsNullOrEmpty(status) || Enum.TryParse<BookingStatus>(status, out _))
                .WithMessage("Недопустимый статус. Допустимые значения: 'Забронировано', 'Недоступно', 'Свободно'.");
        }
    }
}
