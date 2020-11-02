using FluentValidation;
using System;
using TeachMe.Core.Resources;

namespace TeachMe.API.Models.DTO.Validators
{
    public class CargoValidador : AbstractValidator<CargoDTO>
    {
        private readonly IResourceLocalizer _resource;
        public CargoValidador(IResourceLocalizer resource)
        {
            _resource = resource;

            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty()
                .Must(x => x != Guid.Empty)
                .WithMessage(string.Format(_resource.GetString("FIELD_REQUIRED"), "Id do cargo"));
        }
    }
}
