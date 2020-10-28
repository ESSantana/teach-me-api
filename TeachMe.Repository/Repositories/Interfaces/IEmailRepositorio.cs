using System;

namespace TeachMe.Repository.Repositories.Interfaces
{
    public interface IEmailRepositorio
    {
        void NotificarCadastro(string email, string nome, Guid id);
        
    }
}