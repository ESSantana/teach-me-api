using System;

namespace TeachMe.Repository.Repositories.Interfaces
{
    public interface IEmailRepositorio
    {
        void NotificarCadastro(string email, string nome, Guid id);
        void NotificarMudancaPerfilProfessor(string email, string nome);
        void NotificarAlteracaoSenha(string email, string novaSenha);
    }
}