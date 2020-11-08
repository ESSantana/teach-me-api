using FluentValidation;
using TeachMe.Core.Resources;
using TeachMe.Service.Services.Interfaces;

namespace TeachMe.API.Models.DTO.Validators
{
    public class UsuarioValidador : AbstractValidator<UsuarioDTO>
    {
        private readonly IEscolaridadeServico _escolaridadeServico;
        private readonly IResourceLocalizer _localizer;

        public UsuarioValidador(IEscolaridadeServico escolaridadeServico, IResourceLocalizer localizer)
        {
            _localizer = localizer;
            _escolaridadeServico = escolaridadeServico;

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

            RuleFor(x => x.EscolaridadeId)
                .NotNull()
                .NotEmpty()
                .WithMessage(string.Format(_localizer.GetString("FIELD_REQUIRED"), "Escolaridade"))
                .Must(x => _escolaridadeServico.ObterEscolaridadePorId(x) != null)
                .WithMessage(string.Format(_localizer.GetString("INEXISTENT_ID"), "Modelo de escolaridade", 100));

            RuleFor(x => x.Telefone)
                .MaximumLength(255)
                .WithMessage(string.Format(_localizer.GetString("SIZE_RULE"), "Telefone", 11));
        }
    }
}
