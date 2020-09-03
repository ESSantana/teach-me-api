using FluentValidation;
using TeachMe.Core.Resources;

namespace TeachMe.API.Models.DTO.Validators
{
    public class UsuarioValidator : AbstractValidator<UsuarioDTO>
    {
        private readonly IResourceLocalizer _localizer;
        public UsuarioValidator(IResourceLocalizer localizer)
        {
            _localizer = localizer;

            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(0)
                .WithMessage(string.Format(_localizer.GetString("FIELD_FORMAT"), "ID"));

            RuleFor(x => x.Nome)
                .NotNull()
                .NotEmpty()
                .WithMessage(string.Format(_localizer.GetString("FIELD_REQUIRED"), "Nome"))
                .MaximumLength(50)
                .WithMessage(string.Format(_localizer.GetString("SIZE_RULE"), "Nome", 80));

            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage(string.Format(_localizer.GetString("FIELD_REQUIRED"), "Email"))
                .MaximumLength(50)
                .WithMessage(string.Format(_localizer.GetString("SIZE_RULE"), "Email", 100));

            RuleFor(x => x.NuDocumento)
                .NotNull()
                .NotEmpty()
                .WithMessage(string.Format(_localizer.GetString("FIELD_REQUIRED"), "Número do documento"))
                .MaximumLength(50)
                .WithMessage(string.Format(_localizer.GetString("SIZE_RULE"), "Número do documento", 11));

            RuleFor(x => x.TipoDocumento)
                .NotNull()
                .NotEmpty()
                .WithMessage(string.Format(_localizer.GetString("FIELD_REQUIRED"), "Tipo de documento"))
                .MaximumLength(50)
                .WithMessage(string.Format(_localizer.GetString("SIZE_RULE"), "Tipo de documento", 3));

            RuleFor(x => x.Escolaridade)
                .NotNull()
                .NotEmpty()
                .WithMessage(string.Format(_localizer.GetString("FIELD_REQUIRED"), "Escolaridade"))
                .MaximumLength(50)
                .WithMessage(string.Format(_localizer.GetString("SIZE_RULE"), "Escolaridade", 100));

            RuleFor(x => x.Telefone)
                .MaximumLength(255)
                .WithMessage(string.Format(_localizer.GetString("SIZE_RULE"), "Telefone", 11));
        }
    }
}
