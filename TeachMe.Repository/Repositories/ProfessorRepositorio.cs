using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using TeachMe.Core.Dominio;
using TeachMe.Core.Utils;
using TeachMe.Repository.Context;
using TeachMe.Repository.Repositories.Interfaces;

namespace TeachMe.Repository.Repositories
{
    public class ProfessorRepositorio : IProfessorRepositorio
    {
        private readonly TeachDbContext _contexto;
        private readonly ILogger<ProfessorRepositorio> _logger;

        public ProfessorRepositorio(TeachDbContext contexto, ILogger<ProfessorRepositorio> logger)
        {
            _contexto = contexto;
            _logger = logger;
        }

        public List<Professor> ObterProfessores(long id = 0, string nome = null, string disciplina = null)
        {
            _logger.LogDebug("ObterProfessores");
            try
            {
                Expression<Func<ProfessorDisciplina, bool>> predicado = ExpressionExtension.Query<ProfessorDisciplina>();
                predicado = FiltrarNome(nome, predicado);
                predicado = FiltrarDisciplina(disciplina, predicado);
                predicado = FiltrarId(id, predicado);

                var resultado = _contexto.Set<ProfessorDisciplina>()
                    .Include(prof => prof.Professor)
                        .ThenInclude(usr => usr.Usuario)
                            .ThenInclude(esc => esc.Escolaridade)
                    .Include(prof => prof.Professor)
                        .ThenInclude(mod => mod.ModalidadeEnsino)
                    .Include(prof => prof.Professor)
                        .ThenInclude(esc => esc.EscolaridaPubAlvo)
                    .Include(prof => prof.Professor)
                        .ThenInclude(ava => ava.AvaliacaoProfessor)
                    .Include(disc => disc.Disciplina)
                    .Where(predicado)
                    .AsNoTracking()
                    .ToList();

                var professores = resultado.GroupBy(x => x.ProfessorId)
                    .Select(y => new Professor(y.ToList().FirstOrDefault(x => x.Professor != null).Professor)
                    {
                        Disciplinas = y.ToList().Select(xy => xy.Disciplina).ToList()
                    }).ToList();

                professores.ForEach(x => x.Usuario.Senha = string.Empty);

                return professores;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, $"ObterProfessores Erro: {ex.Message}");
                throw;
            }
        }

        public Professor TornarProfessor(Professor professor)
        {
            _logger.LogDebug("TornarProfessor");
            try
            {
                var professorId = _contexto.Set<Cargo>().SingleOrDefault(x => x.Descricao.ToUpper().Equals("PROFESSOR")).Id;
                var usuario = _contexto.Set<Usuario>().SingleOrDefault(x => x.Id.Equals(professor.UsuarioId));
                usuario.CargoId = professorId;

                _contexto.Update(usuario);
                _contexto.Add(professor);

                _contexto.SaveChanges();

                if (professor.Id != 0)
                {
                    professor.Disciplinas.ForEach(disc =>
                    {
                        _contexto.Add(new ProfessorDisciplina
                        {
                            DisciplinaId = disc.Id,
                            ProfessorId = professor.Id
                        });
                    });
                }

                _contexto.SaveChanges();

                return professor;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, $"TornarProfessor Erro: {ex.Message}");
                _contexto.Database.RollbackTransaction();
                throw;
            }
        }

        private Expression<Func<ProfessorDisciplina, bool>> FiltrarNome(string nome, Expression<Func<ProfessorDisciplina, bool>> predicado)
        {
            if (!string.IsNullOrEmpty(nome))
            {
                predicado = predicado.And(x => x.Professor.Usuario.Nome.ToUpper().Contains(nome.ToUpper()));
            }

            return predicado;
        }

        private Expression<Func<ProfessorDisciplina, bool>> FiltrarDisciplina(string disciplina, Expression<Func<ProfessorDisciplina, bool>> predicado)
        {
            if (!string.IsNullOrEmpty(disciplina))
            {
                predicado = predicado.And(x => x.Disciplina.Descricao.ToUpper().Contains(disciplina.ToUpper()));
            }

            return predicado;
        }

        private Expression<Func<ProfessorDisciplina, bool>> FiltrarId(long Id, Expression<Func<ProfessorDisciplina, bool>> predicado)
        {
            if (Id > 0)
            {
                predicado = predicado.And(x => x.Professor.Id.Equals(Id));
            }

            return predicado;
        }

    }
}
