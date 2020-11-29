using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using TeachMe.Core.Dominio;
using TeachMe.Core.Exceptions;
using TeachMe.Core.Resources;
using TeachMe.Repository.Repositories.Interfaces;
using TeachMe.Service.Services.Interfaces;

namespace TeachMe.Service.Services
{
    public class UsuarioServico : IUsuarioServico
    {
        private readonly ILogger<UsuarioServico> _logger;
        private readonly IResourceLocalizer _resource;
        private readonly IUsuarioRepositorio _repositorio;
        private readonly IEmailRepositorio _emailRepositorio;
        private readonly IValidacaoRepositorio _validacaoRepositorio;

        public UsuarioServico(IUsuarioRepositorio repositorio, IEmailRepositorio emailRepositorio, IValidacaoRepositorio validacaoRepositorio, ILogger<UsuarioServico> logger, IResourceLocalizer resource)
        {
            _repositorio = repositorio;
            _emailRepositorio = emailRepositorio;
            _validacaoRepositorio = validacaoRepositorio;
            _logger = logger;
            _resource = resource;
        }

        public Usuario Login(string email, string senha)
        {
            _logger.LogDebug("Login");

            var emailParticionado = email.Split("@");

            //TO DO: Substituir por ReGex
            if ((emailParticionado.Length == 2 && emailParticionado[1].Split(".").Length == 2) || long.TryParse(email, out long _)
              && !string.IsNullOrEmpty(senha))
            {
                var resultado = _repositorio.Login(email, EncriptarSenha(senha));

                _logger.LogDebug($"Login: usuário existente? {resultado != null}");
                return resultado;
            }

            throw new BusinessException(_resource.GetString("MISSING_PARAM"));
        }

        public bool ValidarCadastro(Guid validacaoId)
        {
            _logger.LogDebug("ValidarCadastro");

            if (validacaoId == Guid.Empty)
            {
                return false;
            }

            var resultado = _validacaoRepositorio.ValidarCadastro(validacaoId);

            _logger.LogDebug($"ValidarCadastro Usuario validado? {resultado}");
            return resultado;
        }

        public List<Usuario> ObterTodos()
        {
            _logger.LogDebug("ObterTodos");
            var resultado = _repositorio.ObterTodos();

            _logger.LogDebug($"ObterTodos resultado: {resultado.Count} usuário(s)");
            return resultado;
        }

        public Usuario ObterPorId(long Id, bool processoInterno = false)
        {
            _logger.LogDebug("ObterPorId");
            if (Id < 1)
            {
                throw new BusinessException(_resource.GetString("INVALID_ID"));
            }

            var resultado = _repositorio.ObterPorId(Id, processoInterno);

            _logger.LogDebug($"ObterPorId resultado com sucesso? {resultado != null}");
            return resultado;
        }

        public Usuario Cadastrar(Usuario usuario)
        {
            _logger.LogDebug("Cadastrar");

            var usuarioCadastrado = _repositorio.VerificarExistencia(usuario.Email, usuario.NuDocumento);

            if (usuarioCadastrado)
            {
                _logger.LogDebug("Usuário já cadastrado");
                throw new BusinessException(_resource.GetString("USER_EXISTING"));
            }

            usuario.Senha = EncriptarSenha(usuario.Senha);

            var resultado = _repositorio.Cadastrar(usuario);

            if (resultado != null)
            {
                var validacao = new EmailValidacao
                {
                    Id = Guid.NewGuid(),
                    UsuarioId = resultado.Id,
                    Valido = false
                };

                var idValidacao = _validacaoRepositorio.CriarValidador(validacao);
                _emailRepositorio.NotificarCadastro(usuario.Email, usuario.Nome, idValidacao);
            }

            _logger.LogDebug($"Cadastrar: {resultado} usuário cadastrado");

            return resultado;
        }

        public int Excluir(long Id)
        {
            _logger.LogDebug("Excluir");
            var usuarioParaDeletar = ObterPorId(Id);

            if (usuarioParaDeletar == null)
            {
                _logger.LogWarning("ID Inválido");
                throw new BusinessException(_resource.GetString("INVALID_ID"));
            }

            var resultado = _repositorio.Excluir(Id);
            _logger.LogDebug($"Excluir: usuario com id({Id}) excluído");

            return resultado;
        }

        public Usuario Alterar(Usuario usuario)
        {
            _logger.LogDebug("Alterar");

            if (usuario.Id < 1)
            {
                _logger.LogWarning("ID Inválido");
                throw new BusinessException(_resource.GetString("INVALID_ID"));
            }

            var usuarioAtual = ObterPorId(usuario.Id);

            if (usuarioAtual == null)
            {
                _logger.LogWarning("ID Inválido");
                throw new BusinessException(_resource.GetString("INVALID_ID"));
            }

            usuario.Senha = EncriptarSenha(usuario.Senha);

            var resultado = _repositorio.Alterar(usuario);
            _logger.LogDebug($"Alterado com sucesso? {!string.IsNullOrEmpty(resultado.Nome)}");

            return resultado;

        }

        public bool RecuperarSenha(string email, string tipoDocumento, string documento)
        {
            try
            {
                var rand = new Random();

                List<int> getCharCode(int min, int max, int amount)
                {
                    var result = new List<int>();

                    for (int i = 0; i < amount; i++)
                    {
                        result.Add(rand.Next(min, max));
                    }
                    return result;
                };

                var usuarioResult = _repositorio.Obter(x => x.Email.Equals(email) && x.TipoDocumento.Equals(tipoDocumento.ToUpper()) && x.NuDocumento.Equals(documento));

                if (usuarioResult.Count != 1)
                {
                    throw new BusinessException(string.Format(_resource.GetString("INEXISTENT_ID"), "Usuário"));
                }

                var usuario = usuarioResult[0];

                var listCharCode = new List<int>();
                listCharCode.AddRange(getCharCode(97, 122, 2));
                listCharCode.AddRange(getCharCode(65, 90, 2));
                listCharCode.AddRange(getCharCode(48, 57, 2));
                listCharCode.AddRange(getCharCode(97, 122, 2));
                listCharCode.AddRange(getCharCode(65, 90, 2));

                var novaSenha = string.Join("", listCharCode.Select(x => Convert.ToChar(x)));

                usuario.Senha = EncriptarSenha(novaSenha);

                var usuarioAlterado = _repositorio.Alterar(usuario);

                if (usuarioAlterado != null)
                {
                    _emailRepositorio.NotificarAlteracaoSenha(email, novaSenha);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{typeof(UsuarioServico)} - {nameof(RecuperarSenha)}: {ex.Message}");
                throw new BusinessException(ex.Message);
            }
        }

        private string EncriptarSenha(string senha)
        {
            var encriptador = SHA256.Create();

            var hash = encriptador.ComputeHash(Encoding.ASCII.GetBytes(senha)).ToList();

            return string.Join("", hash.Select(x => x.ToString("X2")).ToList()).ToLower();
        }
    }
}
