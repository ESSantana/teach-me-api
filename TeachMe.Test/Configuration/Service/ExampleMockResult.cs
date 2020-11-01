using System;
using System.Collections.Generic;
using TeachMe.Core.Dominio;

namespace TeachMe.Test.Configuration.Service
{
    public static class ExampleMockResult
    {
        public static List<Usuario> Get()
        {
            return new List<Usuario>
            {
                new Usuario
                {
                    Id = 1,
                    Nome = "Nome Mock 1",
                    Email="example1@mail.com",
                    Senha="SenhaSegura",
                    DataNascimento= new DateTime(1990,01,01),
                    NuDocumento="111111111111",
                    Telefone="71999887744",
                    TipoDocumento = "CPF",
                    Escolaridade = "Escolaridade Mock 1"
                },
                new Usuario
                {
                    Id = 2,
                    Nome = "Nome Mock 2",
                    Email="example2@mail.com",
                    Senha="SenhaSegura",
                    DataNascimento= new DateTime(1990,01,01),
                    NuDocumento="111111111111",
                    Telefone="71999887744",
                    TipoDocumento = "CPF",
                    Escolaridade = "Escolaridade Mock 2"
                },
                new Usuario
                {
                    Id = 3,
                    Nome = "Nome Mock 3",
                    Email="example3@mail.com",
                    Senha="SenhaSegura",
                    DataNascimento= new DateTime(1990,01,01),
                    NuDocumento="111111111111",
                    Telefone="71999887744",
                    TipoDocumento = "CPF",
                    Escolaridade = "Escolaridade Mock 3"
                }
            };
        }
    }
}
