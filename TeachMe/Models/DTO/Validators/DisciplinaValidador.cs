using FluentValidation;
using System;
using TeachMe.Core.Resources;

namespace TeachMe.API.Models.DTO.Validators
{
    public class DisciplinaValidador : AbstractValidator<DisciplinaDTO>
    {
        private readonly IResourceLocalizer _resource;
        public DisciplinaValidador(IResourceLocalizer resource)
        {
            _resource = resource;

            RuleFor(x => x.Id)
               .NotNull()
               .NotEmpty()
               .Must(x => x != Guid.Empty)
               .WithMessage(string.Format(_resource.GetString("FIELD_REQUIRED"), "Id da disciplina"));
        }
    }
}
