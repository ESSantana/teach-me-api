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
                    EscolaridadeId = new Guid("2755849d-af9a-7a4a-bb6a-d70c5edea77d")
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
                    EscolaridadeId = new Guid("edcb1b45-2052-0e48-b618-c5ae51c42e46")
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
                    EscolaridadeId = new Guid("0bacfd5f-d7c8-264b-ae04-7447ad12002f")
                }
            };
        }
    }
}
