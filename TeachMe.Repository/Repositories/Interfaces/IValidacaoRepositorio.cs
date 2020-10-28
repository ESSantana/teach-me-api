using System;
using TeachMe.Repository.Entities;

namespace TeachMe.Repository.Repositories.Interfaces
{
  public interface IValidacaoRepositorio
  {
    Guid CriarValidador(EmailValidacao validacao);
    bool ValidarCadastro(Guid validacaoId);
  }
}