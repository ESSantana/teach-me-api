using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using TeachMe.Repository.Context;
using TeachMe.Repository.Entities;
using TeachMe.Repository.Repositories.Interfaces;
using TeachMe.Repository.Utils;

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

        public List<Usuario> ObterProfessores(long id = 0, string nome = null, string disciplina = null)
        {
            _logger.LogDebug("ObterProfessores");
            try
            {
                Expression<Func<ProfessorDisciplina, bool>> predicado = ExpressionExtension.Query<ProfessorDisciplina>();
                predicado = FiltrarNome(nome, predicado);
                predicado = FiltrarDisciplina(disciplina, predicado);
                predicado = FiltrarId(id, predicado);

                var resultado = _contexto.ProfessorDisciplinas
                    .Include(prof => prof.Professor)
                    .Include(disc => disc.Disciplina)
                    .Where(predicado)
                    .AsNoTracking()
                    .ToList();

                var professores = resultado.GroupBy(x => x.ProfessorId)
                    .Select(y => new Usuario(y.ToList().FirstOrDefault(x => x.Professor != null).Professor)
                    {
                        Disciplinas = y.ToList().Select(xy => xy.Disciplina).ToList()
                    }).ToList();

                professores.ForEach(x => x.Senha = string.Empty);

                return professores;

            }
            catch (DbException ex)
            {
                _logger.LogError(ex, $"ObterProfessores Erro: {ex.Message}");
                throw;
            }
        }

        private Expression<Func<ProfessorDisciplina, bool>> FiltrarNome(string nome, Expression<Func<ProfessorDisciplina, bool>> predicado)
        {
            if (!string.IsNullOrEmpty(nome))
            {
                predicado = predicado.And(x => x.Professor.Nome.ToUpper().Contains(nome.ToUpper()));
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
