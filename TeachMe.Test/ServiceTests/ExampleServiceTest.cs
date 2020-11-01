using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using TeachMe.Core.Dominio;
using TeachMe.Core.Resources;
using TeachMe.Core.Services;
using TeachMe.Repository.Entities;
using TeachMe.Repository.Repositories.Interfaces;
using TeachMe.Test.Configuration.Service;

namespace TeachMe.Test.ServiceTests
{
    public class ExampleServiceTest
    {
        private Mock<IUsuarioRepositorio> repository;
        private Mock<IEmailRepositorio> emailRepository;
        private Mock<IValidacaoRepositorio> validacaoRepository;
        private UsuarioServico service;

        [SetUp]
        public void Setup()
        {
            var logger = new Mock<ILogger<UsuarioServico>>();
            var resource = new Mock<IResourceLocalizer>();
            repository = new Mock<IUsuarioRepositorio>();
            emailRepository = new Mock<IEmailRepositorio>();
            validacaoRepository = new Mock<IValidacaoRepositorio>();

            repository.Setup(r => r.ObterTodos()).Returns(ExampleMockResult.Get());
            repository.Setup(r => r.ObterPorId(It.IsAny<long>())).Returns(ExampleMockResult.Get().First());
            repository.Setup(r => r.Cadastrar(It.IsAny<Usuario>())).Returns(ExampleMockResult.Get().First());
            repository.Setup(r => r.Alterar(It.IsAny<Usuario>())).Returns(ExampleMockResult.Get().First());
            repository.Setup(r => r.Excluir(It.IsAny<long>())).Returns(1);

            service = new UsuarioServico(repository.Object, emailRepository.Object, validacaoRepository.Object, logger.Object, resource.Object);
        }

        [Test]
        public void GetExamples_ShouldReturn_ListOfExampleEntity()
        {
            var result = service.ObterTodos();

            Assert.Multiple(() =>
            {
                repository.Verify(r => r.ObterTodos(), Times.Once);
                Assert.NotZero(result.Count);
                Assert.True(result.All(x => !string.IsNullOrEmpty(x.Nome)));
            });
        }

        [Test]
        public void GetExampleById_ShouldReturn_ExampleEntity()
        {
            var result = service.ObterPorId(1);

            Assert.Multiple(() =>
            {
                repository.Verify(r => r.ObterPorId(It.IsAny<long>()), Times.Once);
                Assert.True(!string.IsNullOrEmpty(result.Nome));
            });
        }

        [Test]
        public void GetExampleById_WithInvalidId_ShouldReturn_Null()
        {
            var result = service.ObterPorId(-1);

            Assert.Multiple(() =>
            {
                repository.Verify(r => r.ObterPorId(It.IsAny<long>()), Times.Never);
                Assert.Null(result);
            });
        }

        [Test]
        public void CreateExample_ShouldReturn_NumberOfEntitiesCreated()
        {
            var entry = new Usuario
            {
                Nome = "Nome Mock 1",
                Email = "example1@mail.com",
                Senha = "SenhaSegura",
                DataNascimento = new DateTime(1990, 01, 01),
                NuDocumento = "111111111111",
                Telefone = "71999887744",
                TipoDocumento = "CPF",
                Escolaridade = "Escolaridade Mock 1"

            };

            var result = service.Cadastrar(entry);

            Assert.Multiple(() =>
            {
                repository.Verify(r => r.Cadastrar(It.IsAny<Usuario>()), Times.Once);
                Assert.NotNull(result);
            });
        }

        //[Test]
        //public void CreateExample_WithoutName_ShouldReturn_Zero()
        //{
        //    var entry = new List<Usuario>
        //    {
        //        new Usuario
        //        {
        //            Description = "Mock Description"
        //        }
        //    };
        //    var result = service.Cadastrar(entry);

        //    Assert.Multiple(() =>
        //    {
        //        repository.Verify(r => r.Cadastrar(It.IsAny<Usuario>()), Times.Never);
        //        Assert.Zero(result);
        //    });
        //}

        [Test]
        public void DeleteExample_ShouldReturn_NumberOfEntitiesDeleted()
        {
            var result = service.Excluir(1);

            Assert.Multiple(() =>
            {
                repository.Verify(r => r.Excluir(It.IsAny<long>()), Times.Once);
                Assert.NotZero(result);
            });
        }

        [Test]
        public void DeleteExample_WithInvalidId_ShouldReturn_Zero()
        {
            repository.Setup(x => x.ObterPorId(It.IsAny<long>())).Returns((Usuario)null);

            var result = service.Excluir(3);

            Assert.Multiple(() =>
            {
                repository.Verify(r => r.Excluir(It.IsAny<long>()), Times.Never);
                Assert.Zero(result);
            });
        }

        [Test]
        public void ModifyExample_WithoutId_ShouldReturn_Null()
        {
            var usuario = new Usuario
            {
                Nome = "Nome Mock 1",
                Email = "example1@mail.com",
                Senha = "SenhaSegura",
                DataNascimento = new DateTime(1990, 01, 01),
                NuDocumento = "111111111111",
                Telefone = "71999887744",
                TipoDocumento = "CPF",
                Escolaridade = "Escolaridade Mock 1"
            };

            var result = service.Alterar(usuario);

            Assert.Multiple(() =>
            {
                repository.Verify(r => r.Alterar(It.IsAny<Usuario>()), Times.Never);
                Assert.Null(result);
            });
        }

        [Test]
        public void ModifyExample_WithInvalidId_ShouldReturn_Null()
        {
            repository.Setup(x => x.ObterPorId(It.IsAny<long>())).Returns((Usuario)null);

            var usuario = new Usuario
            {
                Id = 7,
                Nome = "Nome Mock 1",
                Email = "example1@mail.com",
                Senha = "SenhaSegura",
                DataNascimento = new DateTime(1990, 01, 01),
                NuDocumento = "111111111111",
                Telefone = "71999887744",
                TipoDocumento = "CPF",
                Escolaridade = "Escolaridade Mock 1"
            };

            var result = service.Alterar(usuario);

            Assert.Multiple(() =>
            {
                repository.Verify(r => r.Alterar(It.IsAny<Usuario>()), Times.Never);
                Assert.Null(result);
            });
        }

        [Test]
        public void ModifyExample_ShouldReturn_ExampleEntity()
        {
            var usuario = new Usuario
            {
                Id = 1,
                Nome = "Nome Mock 1",
                Email = "example1@mail.com",
                Senha = "SenhaSegura",
                DataNascimento = new DateTime(1990, 01, 01),
                NuDocumento = "111111111111",
                Telefone = "71999887744",
                TipoDocumento = "CPF",
                Escolaridade = "Escolaridade Mock 1"
            };

            var result = service.Alterar(usuario);

            Assert.Multiple(() =>
            {
                repository.Verify(r => r.Alterar(It.IsAny<Usuario>()), Times.Once);
                Assert.NotNull(result);
            });
        }
    }
}
