using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace ResourceService.Api.Application.AppData.Command.CreateResource
{
    public class CreateResourceCommandValidator : AbstractValidator<CreateResourceCommand>
    {
        public CreateResourceCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Название оборудования обязательно.")
                .MaximumLength(100).WithMessage("Название оборудования не должно превышать 100 символов.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Описание не должно превышать 500 символов.");

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Оборудование должено быть указано по локации.");
        }
    }

}
