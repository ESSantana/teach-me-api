using System;
using TeachMe.Core.Dominio;

namespace TeachMe.Repository.Repositories.Interfaces
{
    public interface IValidacaoRepositorio
    {
        Guid CriarValidador(EmailValidacao validacao);
        bool ValidarCadastro(Guid validacaoId);
        bool ExcluirValidador(Guid validacaoId);
    }
}