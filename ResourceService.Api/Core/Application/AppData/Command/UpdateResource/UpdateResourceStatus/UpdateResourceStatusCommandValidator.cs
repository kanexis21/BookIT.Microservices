using FluentValidation;

namespace ResourceService.Api.Application.AppData.Command.UpdateResource.UpdateResourceStatus
{
    public class UpdateResourceStatusCommandValidator : AbstractValidator<UpdateResourceStatusCommand>
    {
        public UpdateResourceStatusCommandValidator()
        {
            RuleFor(x => x.ResourceId).NotEmpty();
            RuleFor(x => x.Status).IsInEnum();
        }
    }

}
