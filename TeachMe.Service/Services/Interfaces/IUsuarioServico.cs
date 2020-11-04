using System;
using System.Collections.Generic;
using TeachMe.Core.Dominio;

namespace TeachMe.Service.Services.Interfaces
{
    public interface IUsuarioServico
    {
        Usuario Login(string email, string senha);
        bool ValidarCadastro(Guid validacaoId);
        List<Usuario> ObterTodos();
        Usuario ObterPorId(long Id, bool processoInterno = false);
        Usuario Cadastrar(Usuario usuario);
        Usuario Alterar(Usuario usuario);
        int Excluir(long Id);
    }
}
