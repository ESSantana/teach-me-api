using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using TeachMe.Core.Entities;
using TeachMe.Repository.Context;
using TeachMe.Repository.Repositories.Interfaces;

namespace TeachMe.Repository.Repositories
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly TeachDbContext _contexto;
        private readonly ILogger<UsuarioRepositorio> _logger;

        public UsuarioRepositorio(TeachDbContext contexto, ILogger<UsuarioRepositorio> logger)
        {
            _contexto = contexto;
            _logger = logger;
        }
        public List<Usuario> ObterTodos()
        {
            _logger.LogDebug("ObterTodos");
            try
            {
                var resultado = _contexto.Usuarios
                    .AsNoTracking()
                    .ToList();

                _logger.LogDebug($"ObterTodos resultado: {resultado.Count}");

                return resultado;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, $"ObterTodos Erro: {ex.Message}");
                throw;
            }
        }

        public Usuario ObterPorId(long Id)
        {
            _logger.LogDebug("ObterPorId");
            try
            {
                var resultado = _contexto.Usuarios
                    .AsNoTracking()
                    .FirstOrDefault(x => x.Id == Id);

                _logger.LogDebug($"ObterPorId com sucesso? {resultado != null}");

                return resultado;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, $"ObterPorId Erro: {ex.Message}");
                throw;
            }
        }

        public int Cadastrar(Usuario usuario)
        {
            _logger.LogDebug("Alterar");
            try
            {
                _contexto.Usuarios.Add(usuario);

                var resultado = _contexto.SaveChanges();

                if (resultado > 0)
                {
                    _logger.LogDebug($"Alterar: {resultado} usuário alterado");

                    return resultado;
                }
                return 0;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, $"Alterar Erro: {ex.Message}");
                throw;
            }
        }

        public int Excluir(long Id)
        {
            _logger.LogDebug("Delete");
            try
            {
                _contexto.Usuarios.Remove(ObterPorId(Id));
                var result = _contexto.SaveChanges();

                _logger.LogDebug($"Delete: entity with id({Id}) deleted");

                return result;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, $"Delete Error: {ex.Message}");
                throw;
            }
        }

        public Usuario Alterar(Usuario usuario)
        {
            _logger.LogDebug("Alterar");
            try
            {
                var usuarioAtual = ObterPorId(usuario.Id);
                usuarioAtual.Nome = usuario.Nome;
                usuarioAtual.DataNascimento = usuario.DataNascimento;
                usuarioAtual.Email = usuario.Email;
                usuarioAtual.Escolaridade = usuario.Escolaridade;
                usuarioAtual.NuDocumento = usuario.NuDocumento;
                usuarioAtual.Senha = usuario.Senha;
                usuarioAtual.Telefone = usuario.Telefone;
                usuarioAtual.TipoDocumento = usuario.TipoDocumento;

                _contexto.Usuarios.Update(usuarioAtual);
                var resultado = _contexto.SaveChanges();

                _logger.LogDebug($"Alterar: {resultado} usuario alterado");

                return resultado == 1
                  ? usuarioAtual
                  : new Usuario();
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, $"Alterar Erro: {ex.Message}");
                throw;
            }
        }
    }
}
