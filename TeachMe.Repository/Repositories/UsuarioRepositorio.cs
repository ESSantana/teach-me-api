using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using TeachMe.Core.Dominio;
using TeachMe.Core.Exceptions;
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

        public Usuario Login(string email, string senha)
        {
            try
            {
                var resultado = _contexto.Set<Usuario>()
                    .Include(x => x.Cargo)
                    .FirstOrDefault(usr => (usr.Email.Equals(email) || usr.NuDocumento.Equals(email)) && usr.Senha.Equals(senha));

                var validado = _contexto.Set<EmailValidacao>().SingleOrDefault(x => x.UsuarioId == resultado.Id).Valido;

                if (!validado)
                {
                    throw new Exception("Conta não validada, por favor verifique sua caixa de email.");
                }

                if (resultado != null)
                {
                    resultado.Senha = string.Empty;
                }

                return resultado;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"VerificarExistencia Erro: {ex.Message}");
                throw new BusinessException(ex.Message);
            }
            
        }

        public List<Usuario> ObterTodos()
        {
            _logger.LogDebug("ObterTodos");
            try
            {
                var resultado = _contexto.Set<Usuario>()
                    .AsNoTracking()
                    .Include(x => x.Cargo)
                    .ToList();

                _logger.LogDebug($"ObterTodos resultado: {resultado.Count}");
                resultado.ForEach(x => x.Senha = string.Empty);

                return resultado;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, $"ObterTodos Erro: {ex.Message}");
                throw;
            }
        }

        public Usuario ObterPorId(long Id, bool processoInterno = false)
        {
            _logger.LogDebug("ObterPorId");
            try
            {
                var resultado = _contexto.Set<Usuario>()
                    .AsNoTracking()
                    .Include(x => x.Cargo)
                    .FirstOrDefault(x => x.Id == Id);

                _logger.LogDebug($"ObterPorId com sucesso? {resultado != null}");

                if (!processoInterno)
                {
                    resultado.Senha = string.Empty;
                }
                return resultado;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, $"ObterPorId Erro: {ex.Message}");
                throw;
            }
        }

        public Usuario Cadastrar(Usuario usuario)
        {
            _logger.LogDebug("Cadastrar");
            try
            {
                var alunoId = _contexto.Set<Cargo>().FirstOrDefault(x => x.Descricao.ToUpper().Equals("ALUNO")).Id;

                usuario.CargoId = alunoId;
                _contexto.Add(usuario);

                var resultado = _contexto.SaveChanges();

                if (resultado > 0)
                {
                    _logger.LogDebug($"Cadastrar: {resultado} usuário alterado");

                    return usuario;
                }
                return null;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, $"Cadastrar Erro: {ex.Message}");
                throw;
            }
        }

        public bool VerificarExistencia(string email, string nudocumento)
        {
            try
            {
                var resultado = _contexto.Set<Usuario>().FirstOrDefault(usr => usr.Email.Equals(email) || usr.NuDocumento.Equals(nudocumento));

                return resultado != null;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, $"VerificarExistencia Erro: {ex.Message}");
                throw;
            }
        }

        public int Excluir(long Id)
        {
            _logger.LogDebug("Delete");
            try
            {
                _contexto.Remove(ObterPorId(Id));
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

                _contexto.Update(usuarioAtual);
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
