using System.Collections.Generic;
using TeachMe.Core.Entities;

namespace TeachMe.Repository.Repositories.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Usuario Login(string email, string senha);
        List<Usuario> ObterTodos();
        Usuario ObterPorId(long Id);
        int Cadastrar(Usuario usuario);
        bool VerificarExistencia(string email, string nudocumento);
        Usuario Alterar(Usuario usuario);
        int Excluir(long Id);
    }
}
