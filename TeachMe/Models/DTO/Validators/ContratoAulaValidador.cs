using FluentValidation;
using System;
using TeachMe.Core.Resources;
using TeachMe.Core.Services.Interfaces;

namespace TeachMe.API.Models.DTO.Validators
{
    public class ContratoAulaValidador : AbstractValidator<ContratoAulaDTO>
    {
        private readonly IResourceLocalizer _localizer;
        private readonly IUsuarioServico _usuarioServico;
        private readonly IProfessorServico _professorServico;

        public ContratoAulaValidador(IResourceLocalizer localizer, IUsuarioServico usuarioServico, IProfessorServico professorServico)
        {
            _localizer = localizer;
            _usuarioServico = usuarioServico;
            _professorServico = professorServico;

            RuleFor(x => x.DataContrato)
                .NotNull()
                .NotEmpty()
                .WithMessage(string.Format(_localizer.GetString("FIELD_REQUIRED"), "Data do contrato"))
                .Must(x => x.Value.Date == DateTime.Now.Date)
                .WithMessage(_localizer.GetString("AULA_CONTRATO_RESTRICT"));

            RuleFor(x => x.DataInicioPrestacao)
                .NotNull()
                .NotEmpty()
                .WithMessage(string.Format(_localizer.GetString("FIELD_REQUIRED"), "Data da aula"))
                .Must(x => x.Value.Date > DateTime.Now.Date)
                .WithMessage(_localizer.GetString("AULA_PRESTACAO_RESTRICT"));

            RuleFor(x => x.AlunoId)
                .Must(alunoId => _usuarioServico.ObterPorId(alunoId, true) != null)
                .WithMessage(string.Format(_localizer.GetString("INEXISTENT_ID"), "Aluno"));

            RuleFor(x => x.ProfessorId)
                .Must(professorId => _professorServico.ObterProfessores(professorId).Count == 1)
                .WithMessage(string.Format(_localizer.GetString("INEXISTENT_ID"), "Professor"));

            RuleFor(x => x.ValorHora)
                .NotNull()
                .NotEmpty()
                .WithMessage(string.Format(_localizer.GetString("FIELD_REQUIRED"), "Valor por Hora"));

            RuleFor(x => x.HorasContratadas)
                .NotNull()
                .NotEmpty()
                .WithMessage(string.Format(_localizer.GetString("FIELD_REQUIRED"), "Horas Contratadas"));
        }
    }
}
