using FluentValidation;

namespace ResourceService.Api.Application.AppData.Command.DeleteResource
{
    public class DeleteResourceCommandValidator : AbstractValidator<DeleteResourceCommand>
    {
        public DeleteResourceCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id обязателен.");
        }
    }
}
