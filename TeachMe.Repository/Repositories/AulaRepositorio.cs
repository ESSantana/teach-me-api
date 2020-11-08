using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using TeachMe.Core.Dominio;
using TeachMe.Repository.Context;
using TeachMe.Repository.Repositories.Interfaces;

namespace TeachMe.Repository.Repositories
{
    public class AulaRepositorio : IAulaRepositorio
    {
        private readonly TeachDbContext _contexto;
        private readonly ILogger<AulaRepositorio> _logger;

        public AulaRepositorio(TeachDbContext contexto, ILogger<AulaRepositorio> logger)
        {
            _contexto = contexto;
            _logger = logger;
        }

        public ContratoAula ContratarAula(ContratoAula contrato)
        {
            try
            {
                _contexto.Add(contrato);
                _contexto.SaveChanges();

                return contrato;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, $"ContratarAula Erro: {ex.Message}");
                throw;
            }
        }

        public ContratoAula ObterAulaParaAvaliarPorId(long aulaId)
        {
            _logger.LogDebug("ObterAulaParaAvaliarPorId");

            try
            {
                var aula = _contexto.Set<ContratoAula>().SingleOrDefault(x => x.Id == aulaId && !x.Avaliado);

                return aula;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, $"ObterAulaParaAvaliarPorId error: {ex.Message}");
                throw;
            }
        }

        public List<ContratoAula> ObterAulaParaAvaliar(long alunoId)
        {
            _logger.LogDebug("ObterAulaParaAvaliar");

            try
            {
                var aulas = _contexto.Set<ContratoAula>()
                    .Where(x => x.AlunoId == alunoId 
                        && DateTime.Now > x.DataFimPrestacao 
                        && !x.Avaliado)
                    .ToList();

                return aulas;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, $"ObterAulaParaAvaliar error: {ex.Message}");
                throw;
            }
        }

        public AvaliacaoProfessor AvaliarProfessor(AvaliacaoProfessor avaliacao)
        {
            _logger.LogDebug("AvaliarProfessor");

            try
            {
                var aula = _contexto.Set<ContratoAula>().SingleOrDefault(x => x.Id == avaliacao.AulaId);
                aula.Avaliado = true;

                _contexto.Add(avaliacao);
                _contexto.Update(aula);

                _contexto.SaveChanges();

                return avaliacao;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, $"AvaliarProfessor error: {ex.Message}");
                throw;
            }
        }

        public bool AulaParaAvaliacao(long alunoId, long professorId, long aulaId)
        {
            try
            {
                var resultado = _contexto.Set<ContratoAula>()
                    .SingleOrDefault(x => x.AlunoId == alunoId
                        && x.ProfessorId == professorId
                        && x.Id == aulaId
                        && !x.Avaliado);

                return resultado != null;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, $"AulaParaAvaliacao error: {ex.Message}");
                throw;
            }
        }
    }
}
