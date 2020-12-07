using FluentValidation;
using System;
using TeachMe.Core.Resources;
using TeachMe.Service.Services.Interfaces;

namespace TeachMe.API.Models.DTO.Validators
{
    public class ContratoAulaValidador : AbstractValidator<ContratoAulaDTO>
    {
        private readonly IResourceLocalizer _resource;
        private readonly IUsuarioServico _usuarioServico;
        private readonly IProfessorServico _professorServico;

        public ContratoAulaValidador(IResourceLocalizer resource, IUsuarioServico usuarioServico, IProfessorServico professorServico)
        {
            _resource = resource;
            _usuarioServico = usuarioServico;
            _professorServico = professorServico;

            RuleFor(x => x.DataInicioPrestacao)
                .NotNull()
                .NotEmpty()
                .WithMessage(string.Format(_resource.GetString("FIELD_REQUIRED"), "Data da aula"))
                .Must(x => x.Value.Date > DateTime.Now.Date)
                .WithMessage(_resource.GetString("AULA_PRESTACAO_RESTRICT"));

            RuleFor(x => x.AlunoId)
                .Must(alunoId => _usuarioServico.ObterPorId(alunoId, true) != null)
                .WithMessage(string.Format(_resource.GetString("INEXISTENT_ID"), "Aluno"));

            RuleFor(x => x.ProfessorId)
                .Must(professorId => _professorServico.ObterProfessores(0, professorId).Count == 1)
                .WithMessage(string.Format(_resource.GetString("INEXISTENT_ID"), "Professor"));

            RuleFor(x => x.ValorHora)
                .NotNull()
                .NotEmpty()
                .WithMessage(string.Format(_resource.GetString("FIELD_REQUIRED"), "Valor por Hora"))
                .Must(x => x >= 10 && x <= 200)
                .WithMessage(string.Format(_resource.GetString("VALUE_HOUR_RULE"), "10", "200"));

            RuleFor(x => x.HorasContratadas)
                .NotNull()
                .NotEmpty()
                .WithMessage(string.Format(_resource.GetString("FIELD_REQUIRED"), "Horas Contratadas"))
                .Must(x => x >= 1)
                .WithMessage(string.Format(_resource.GetString("MINIMUM_HOUR_RESTRICT"), "1"));
        }
    }
}
