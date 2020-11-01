using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TeachMe.API.Controllers;
using TeachMe.API.Models.DTO;
using TeachMe.Core.Dominio;
using TeachMe.Core.Services.Interfaces;
using TeachMe.Test.Configuration.Service;

namespace TeachMe.Test.ControllerTests
{
    public class ExampleControllerTest
    {
        private UsuarioController controller;
        private Mock<IUsuarioServico> service;

        [SetUp]
        public void Setup()
        {
            var mapper = new Mock<IMapper>();
            var logger = new Mock<ILogger<UsuarioController>>();
            service = new Mock<IUsuarioServico>();

            service.Setup(r => r.ObterTodos()).Returns(ExampleMockResult.Get());
            service.Setup(r => r.ObterPorId(It.IsAny<long>())).Returns(ExampleMockResult.Get().First());
            service.Setup(r => r.Cadastrar(It.IsAny<Usuario>())).Returns(ExampleMockResult.Get().First());
            service.Setup(r => r.Alterar(It.IsAny<Usuario>())).Returns(ExampleMockResult.Get().First());
            service.Setup(r => r.Excluir(It.IsAny<long>())).Returns(1);

            controller = new UsuarioController(service.Object, logger.Object, mapper.Object);
        }

        [Test]
        public void Get_ShouldReturn_ListOfExampleDTO()
        {
            var result = controller.ObterTodos();

            Assert.Multiple(() =>
            {
                service.Verify(s => s.ObterTodos(), Times.Once);
                Assert.NotNull(result);
                Assert.IsInstanceOf(typeof(OkObjectResult), result.Result);
            });
        }

        [Test]
        public void Get_ShouldReturn_NoContentResult()
        {
            service.Setup(r => r.ObterTodos()).Returns(new List<Usuario>());

            var result = controller.ObterTodos();

            Assert.Multiple(() =>
            {
                service.Verify(s => s.ObterTodos(), Times.Once);
                Assert.NotNull(result);
                Assert.IsInstanceOf(typeof(NoContentResult), result.Result);
            });
        }

        [Test]
        public void GetById_ShouldReturn_ExampleDTO()
        {
            var result = controller.Obter(1);

            Assert.Multiple(() =>
            {
                service.Verify(s => s.ObterPorId(It.IsAny<long>()), Times.Once);
                Assert.NotNull(result);
                Assert.IsInstanceOf(typeof(OkObjectResult), result.Result);
            });
        }

        [Test]
        public void GetByID_ShouldReturn_NoContentResult()
        {
            service.Setup(r => r.ObterPorId(It.IsAny<long>())).Returns((Usuario)null);

            var result = controller.Obter(1);

            Assert.Multiple(() =>
            {
                service.Verify(s => s.ObterPorId(It.IsAny<long>()), Times.Once);
                Assert.NotNull(result);
                Assert.IsInstanceOf(typeof(NoContentResult), result.Result);
            });
        }

        [Test]
        public void Create_ShouldReturn_TotalResult()
        {
            var usuario = new UsuarioDTO
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

            var result = controller.Cadastrar(usuario);

            Assert.Multiple(() =>
            {
                service.Verify(s => s.Cadastrar(It.IsAny<Usuario>()), Times.Once);
                Assert.NotNull(result);
                Assert.IsInstanceOf(typeof(OkObjectResult), result.Result);
            });
        }

        [Test]
        public void Create_ShouldReturn_NoContentResult()
        {
            service.Setup(r => r.Cadastrar(It.IsAny<Usuario>())).Returns((Usuario)null);

            var entity = new UsuarioDTO
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

            var result = controller.Cadastrar(entity);

            Assert.Multiple(() =>
            {
                service.Verify(s => s.Cadastrar(It.IsAny<Usuario>()), Times.Once);
                Assert.NotNull(result);
                Assert.IsInstanceOf(typeof(NoContentResult), result.Result);
            });
        }

        [Test]
        public void Modify_ShouldReturn_ExampleDTO()
        {
            var usuario = new UsuarioDTO
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

            var result = controller.Alterar(usuario);

            Assert.Multiple(() =>
            {
                service.Verify(s => s.Alterar(It.IsAny<Usuario>()), Times.Once);
                Assert.NotNull(result);
                Assert.IsInstanceOf(typeof(OkObjectResult), result.Result);
            });
        }

        [Test]
        public void Modify_ShouldReturn_NoContentResult()
        {
            service.Setup(r => r.Alterar(It.IsAny<Usuario>())).Returns((Usuario) null);

            var usuario = new UsuarioDTO
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

            var result = controller.Alterar(usuario);

            Assert.Multiple(() =>
            {
                service.Verify(s => s.Alterar(It.IsAny<Usuario>()), Times.Once);
                Assert.NotNull(result);
                Assert.IsInstanceOf(typeof(NoContentResult), result.Result);
            });
        }

        [Test]
        public void Delete_ShouldReturn_TotalDeleted()
        {
            var result = controller.Excluir(1);

            Assert.Multiple(() =>
            {
                service.Verify(s => s.Excluir(It.IsAny<long>()), Times.Once);
                Assert.NotNull(result);
                Assert.IsInstanceOf(typeof(OkObjectResult), result.Result);
            });
        }
    }
}
