using FluentValidation;
using TeachMe.Core.Resources;
using TeachMe.Service.Services.Interfaces;

namespace TeachMe.API.Models.DTO.Validators
{
    public class AvaliacaoProfessorValidador : AbstractValidator<AvaliacaoProfessorDTO>
    {
        private readonly IResourceLocalizer _resource;
        private readonly IProfessorServico _professorServico;
        private readonly IUsuarioServico _usuarioServico;
        private readonly IAulaServico _aulaServico;

        public AvaliacaoProfessorValidador(IProfessorServico professorServico, IUsuarioServico usuarioServico, IAulaServico aulaServico, IResourceLocalizer resource)
        {
            _professorServico = professorServico;
            _aulaServico = aulaServico;
            _usuarioServico = usuarioServico;
            _resource = resource;

            RuleFor(x => x.Nota)
                .NotNull()
                .NotEmpty()
                .Must(x => x <= 10 && x > 0)
                .WithMessage(_resource.GetString("RATING_REQUIRED"));


            RuleFor(x => x.ProfessorId)
                .NotEmpty()
                .NotNull()
                .WithMessage(string.Format(_resource.GetString("FIELD_REQUIRED"), "Id do Professor"))
                .Must(x => _professorServico.ObterProfessores(x).Count == 1)
                .WithMessage(string.Format(_resource.GetString("INEXISTENT_ID"), "Professor"));

            RuleFor(x => x.AlunoId)
                .NotEmpty()
                .NotNull()
                .WithMessage(string.Format(_resource.GetString("FIELD_REQUIRED"), "Id do Aluno"))
                .Must(x => _usuarioServico.ObterPorId(x) != null)
                .WithMessage(string.Format(_resource.GetString("INEXISTENT_ID"), "Aluno"));

            RuleFor(x => x.AulaId)
                .NotEmpty()
                .NotNull()
                .WithMessage(string.Format(_resource.GetString("FIELD_REQUIRED"), "Id da Aula"))
                .Must(x => _aulaServico.ObterAulaParaAvaliarPorId(x) != null)
                .WithMessage(string.Format(_resource.GetString("INEXISTENT_ID"), "Professor"));
        }
    }
}
