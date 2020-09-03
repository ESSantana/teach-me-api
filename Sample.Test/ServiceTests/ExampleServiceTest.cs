using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using TeachMe.Core.Entities;
using TeachMe.Core.Services;
using TeachMe.Repository.Repositories.Interfaces;
using TeachMe.Test.Configuration.Service;
using System.Collections.Generic;
using System.Linq;

namespace TeachMe.Test.ServiceTests
{
    public class ExampleServiceTest
    {
        private Mock<IUsuarioRepositorio> repository;
        private UsuarioServico service;

        [SetUp]
        public void Setup()
        {
            var logger = new Mock<ILogger<UsuarioServico>>();
            repository = new Mock<IUsuarioRepositorio>();

            repository.Setup(r => r.ObterTodos()).Returns(ExampleMockResult.Get());
            repository.Setup(r => r.ObterPorId(It.IsAny<long>())).Returns(ExampleMockResult.Get().First());
            repository.Setup(r => r.Alterar(It.IsAny<List<Usuario>>())).Returns(1);
            repository.Setup(r => r.Alterar(It.IsAny<Usuario>())).Returns(ExampleMockResult.Get().First());
            repository.Setup(r => r.Excluir(It.IsAny<long>())).Returns(1);

            service = new UsuarioServico(repository.Object, logger.Object);
        }

        [Test]
        public void GetExamples_ShouldReturn_ListOfExampleEntity()
        {
            var result = service.ObterTodos();

            Assert.Multiple(() =>
            {
                repository.Verify(r => r.ObterTodos(), Times.Once);
                Assert.NotZero(result.Count);
                Assert.True(result.All(x => !string.IsNullOrEmpty(x.Name)));
            });
        }

        [Test]
        public void GetExampleById_ShouldReturn_ExampleEntity()
        {
            var result = service.ObterPorId(1);

            Assert.Multiple(() =>
            {
                repository.Verify(r => r.ObterPorId(It.IsAny<long>()), Times.Once);
                Assert.True(!string.IsNullOrEmpty(result.Name));
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
            var entry = new List<Usuario>
            {
                new Usuario
                {
                    Name = "Mock Name",
                    Description = "Mock Description"
                }
            };
            var result = service.Cadastrar(entry);

            Assert.Multiple(() =>
            {
                repository.Verify(r => r.Alterar(It.IsAny<List<Usuario>>()), Times.Once);
                Assert.NotZero(result);
            });
        }

        [Test]
        public void CreateExample_WithoutName_ShouldReturn_Zero()
        {
            var entry = new List<Usuario>
            {
                new Usuario
                {
                    Description = "Mock Description"
                }
            };
            var result = service.Cadastrar(entry);

            Assert.Multiple(() =>
            {
                repository.Verify(r => r.Alterar(It.IsAny<List<Usuario>>()), Times.Never);
                Assert.Zero(result);
            });
        }

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
            var entity = new Usuario
            {
                Name = "Mock Name",
                Description = "Mock Description"
            };

            var result = service.Alterar(entity);

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

            var entity = new Usuario
            {
                Id = 3,
                Name = "Mock Name",
                Description = "Mock Description"
            };

            var result = service.Alterar(entity);

            Assert.Multiple(() =>
            {
                repository.Verify(r => r.Alterar(It.IsAny<Usuario>()), Times.Never);
                Assert.Null(result);
            });
        }

        [Test]
        public void ModifyExample_ShouldReturn_ExampleEntity()
        {
            var entity = new Usuario
            {
                Id = 1,
                Name = "Mock Name",
                Description = "Mock Description"
            };

            var result = service.Alterar(entity);

            Assert.Multiple(() =>
            {
                repository.Verify(r => r.Alterar(It.IsAny<Usuario>()), Times.Once);
                Assert.NotNull(result);
            });
        }
    }
}
