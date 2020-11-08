using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using TeachMe.Core.Dominio;
using TeachMe.Repository.Context;
using TeachMe.Repository.Repositories.Interfaces;

namespace TeachMe.Repository.Repositories
{
    public class ValidacaoRepositorio : IValidacaoRepositorio
    {
        private readonly TeachDbContext _contexto;
        private readonly ILogger<ValidacaoRepositorio> _logger;

        public ValidacaoRepositorio(TeachDbContext contexto, ILogger<ValidacaoRepositorio> logger)
        {
            _contexto = contexto;
            _logger = logger;
        }

        public Guid CriarValidador(EmailValidacao validacao)
        {
            try
            {
                _contexto.Add(validacao);
                _contexto.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $"CriarValidador: {ex.Message}");
                throw;
            }

            return validacao.Id;
        }

        public bool ExcluirValidador(Guid idValidacao)
        {
            try
            {
                var validacao = _contexto.Set<EmailValidacao>().SingleOrDefault(x => x.Id == idValidacao);

                if (validacao != null)
                {
                    _contexto.Remove(validacao);
                    _contexto.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $"CriarValidador: {ex.Message}");
                throw;
            }
        }

        public bool ValidarCadastro(Guid validacaoId)
        {
            try
            {
                var validador = _contexto.Set<EmailValidacao>().SingleOrDefault(x => x.Id == validacaoId);

                if (validador == null)
                {
                    return false;
                }

                validador.Valido = true;

                _contexto.Update(validador);
                return _contexto.SaveChanges() > 0;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $"CriarValidador: {ex.Message}");
                throw;
            }
        }
    }
}