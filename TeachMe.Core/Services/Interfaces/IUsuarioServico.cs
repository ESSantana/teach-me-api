using System.Collections.Generic;
using TeachMe.Repository.Entities;

namespace TeachMe.Core.Services.Interfaces
{
    public interface IUsuarioServico
    {
        Usuario Login(string email, string senha);
        List<Usuario> ObterTodos();
        Usuario ObterPorId(long Id);
        int Cadastrar(Usuario usuario);
        Usuario Alterar(Usuario usuario);
        int Excluir(long Id);
    }
}
