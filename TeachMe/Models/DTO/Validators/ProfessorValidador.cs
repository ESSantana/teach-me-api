using FluentValidation;
using TeachMe.Core.Resources;
using TeachMe.Core.Utils;
using TeachMe.Service.Services.Interfaces;

namespace TeachMe.API.Models.DTO.Validators
{
    public class ProfessorValidador : AbstractValidator<ProfessorDTO>
    {
        private readonly IUsuarioServico _usuarioServico;
        private readonly IEscolaridadeServico _escolaridadeServico;
        private readonly IModalidadeEnsinoServico _modalidadeServico;
        private readonly IResourceLocalizer _resource;

        public ProfessorValidador(IUsuarioServico usuarioServico, IEscolaridadeServico escolaridadeServico, IModalidadeEnsinoServico modalidadeServico, IResourceLocalizer resource)
        {
            _resource = resource;
            _usuarioServico = usuarioServico;
            _escolaridadeServico = escolaridadeServico;
            _modalidadeServico = modalidadeServico;

            RuleFor(x => x.UsuarioId)
                .NotNull()
                .NotEmpty()
                .WithMessage(string.Format(_resource.GetString("FIELD_REQUIRED"),"Id do usuário"))
                .Must(x => _usuarioServico.ObterPorId(x) != null)
                .WithMessage(string.Format(_resource.GetString("INEXISTENT_ID"), "Usuário"));

            RuleFor(x => x.EscolaridaPubAlvoId)
                .NotNull()
                .NotEmpty()
                .WithMessage(string.Format(_resource.GetString("FIELD_REQUIRED"), "Id da escolaridade do público alvo"))
                .Must(x => _escolaridadeServico.ObterEscolaridadePorId(x) != null)
                .WithMessage(string.Format(_resource.GetString("INEXISTENT_ID"), "Modelo de escolaridade"));

            RuleFor(x => x.ModalidadeEnsinoId)
                .NotNull()
                .NotEmpty()
                .WithMessage(string.Format(_resource.GetString("FIELD_REQUIRED"), "Id da Modalidade de ensino"))
                .Must(x => _modalidadeServico.ObterModalidadePorId(x) != null)
                .WithMessage(string.Format(_resource.GetString("INEXISTENT_ID"), "Modelo de modalidade de ensino"));

            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage(string.Format(_resource.GetString("FIELD_REQUIRED"), "Email"))
                .Must(x => EmailUtils.EmailValido(x))
                .WithMessage(string.Format(_resource.GetString("FIELD_FORMAT"), "Email"));

            RuleFor(x => x.Senha)
                .NotNull()
                .NotEmpty()
                .WithMessage(string.Format(_resource.GetString("FIELD_REQUIRED"), "Senha"));

            RuleFor(x => x.ValorHora)
                .NotNull()
                .NotEmpty()
                .WithMessage(string.Format(_resource.GetString("FIELD_REQUIRED"), "Valor por hora/aula"))
                .Must(x => x >= 10 && x <= 200)
                .WithMessage(string.Format(_resource.GetString("VALUE_HOUR_RULE"), "10", "200"));
        }
    }
}
