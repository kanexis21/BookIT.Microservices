using FluentValidation;

namespace RoomService.Api.Core.Application.AppData.Command.CreateRoom
{
    public class CreateRoomCommandValidator : AbstractValidator<CreateRoomCommand>
    {
        public CreateRoomCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Название комнаты обязательно.")
                .MaximumLength(100).WithMessage("Название комнаты не должно превышать 100 символов.");

            RuleFor(x => x.Capacity)
                .GreaterThan(0).WithMessage("Вместимость должна быть больше 0.");

            RuleFor(x => x.Location)
                .MaximumLength(20).WithMessage("Максимальная длина для местоположения — 20 символов.");

            RuleFor(x => x.Description)
                .MaximumLength(300).WithMessage("Максимальная длина для описания — 300 символов.");
        }
    }

}
