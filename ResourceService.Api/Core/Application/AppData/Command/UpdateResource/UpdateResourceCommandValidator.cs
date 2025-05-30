using FluentValidation;

namespace ResourceService.Api.Application.AppData.Command.UpdateResource
{
    public class UpdateResourceCommandValidator : AbstractValidator<UpdateResourceCommand>
    {
        public UpdateResourceCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id обязателен.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Имя обязательно.")
                .MaximumLength(200);

            RuleFor(x => x.Location)
                .MaximumLength(200);

            RuleFor(x => x.Description)
                .MaximumLength(1000);
        }
    }
}
