using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using TeachMe.Core.Entities;
using TeachMe.Core.Services.Interfaces;
using TeachMe.Repository.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace TeachMe.Core.Services
{
    public class UsuarioServico : IUsuarioServico
    {
        private readonly ILogger<UsuarioServico> _logger;
        private readonly IUsuarioRepositorio _repositorio;

        public UsuarioServico(IUsuarioRepositorio repositorio, ILogger<UsuarioServico> logger)
        {
            _repositorio = repositorio;
            _logger = logger;
        }

        public List<Usuario> ObterTodos()
        {
            _logger.LogDebug("ObterTodos");
            var resultado = _repositorio.ObterTodos();

            _logger.LogDebug($"ObterTodos resultado: {resultado.Count} usuário(s)");
            return resultado;
        }

        public Usuario ObterPorId(long Id)
        {
            _logger.LogDebug("ObterPorId");
            if (Id < 1)
            {
                //TO DO: Substituir por lançamento de exceção
                _logger.LogWarning("ID Inválido");
                return null;
            }

            var resultado = _repositorio.ObterPorId(Id);

            _logger.LogDebug($"ObterPorId resultado com sucesso? {resultado != null}");
            return resultado;
        }

        public int Cadastrar(Usuario usuario)
        {
            _logger.LogDebug("Cadastrar");
            //TO DO: Validação de cadastro existente
            
            var resultado = _repositorio.Cadastrar(usuario);

            _logger.LogDebug($"Cadastrar: {resultado} usuário cadastrado");

            return resultado;
        }

        public int Excluir(long Id)
        {
            _logger.LogDebug("Excluir");
            var usuarioParaDeletar = ObterPorId(Id);

            if (usuarioParaDeletar == null)
            {
                //TO DO: Substituir por lançamento de exceção
                _logger.LogWarning("ID inválido");
                return 0;
            }

            var resultado = _repositorio.Excluir(Id);
            _logger.LogDebug($"Excluir: usuario com id({Id}) excluído");

            return resultado;
        }

        public Usuario Alterar(Usuario usuario)
        {
            _logger.LogDebug("Alterar");
            if (usuario.Id > 0)
            {
                var usuarioAtual = ObterPorId(usuario.Id);

                if (usuarioAtual == null)
                {
                    //TO DO: Substituir por lançamento de exceção
                    _logger.LogWarning("ID Inválido");
                    return null;
                }

                var resultado = _repositorio.Alterar(usuario);
                _logger.LogDebug($"Alterado com sucesso? {!string.IsNullOrEmpty(resultado.Nome)}");

                return resultado;
            }

            //TO DO: Substituir por lançamento de exceção
            _logger.LogWarning("ID Inválido");
            return null;
        }
    }
}
