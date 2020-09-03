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
        private Mock<IExampleRepository> repository;
        private ExampleService service;

        [SetUp]
        public void Setup()
        {
            var logger = new Mock<ILogger<ExampleService>>();
            repository = new Mock<IExampleRepository>();

            repository.Setup(r => r.Get()).Returns(ExampleMockResult.Get());
            repository.Setup(r => r.Get(It.IsAny<long>())).Returns(ExampleMockResult.Get().First());
            repository.Setup(r => r.Create(It.IsAny<List<Usuario>>())).Returns(1);
            repository.Setup(r => r.Modify(It.IsAny<Usuario>())).Returns(ExampleMockResult.Get().First());
            repository.Setup(r => r.Delete(It.IsAny<long>())).Returns(1);

            service = new ExampleService(repository.Object, logger.Object);
        }

        [Test]
        public void GetExamples_ShouldReturn_ListOfExampleEntity()
        {
            var result = service.Get();

            Assert.Multiple(() =>
            {
                repository.Verify(r => r.Get(), Times.Once);
                Assert.NotZero(result.Count);
                Assert.True(result.All(x => !string.IsNullOrEmpty(x.Name)));
            });
        }

        [Test]
        public void GetExampleById_ShouldReturn_ExampleEntity()
        {
            var result = service.Get(1);

            Assert.Multiple(() =>
            {
                repository.Verify(r => r.Get(It.IsAny<long>()), Times.Once);
                Assert.True(!string.IsNullOrEmpty(result.Name));
            });
        }

        [Test]
        public void GetExampleById_WithInvalidId_ShouldReturn_Null()
        {
            var result = service.Get(-1);

            Assert.Multiple(() =>
            {
                repository.Verify(r => r.Get(It.IsAny<long>()), Times.Never);
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
            var result = service.Create(entry);

            Assert.Multiple(() =>
            {
                repository.Verify(r => r.Create(It.IsAny<List<Usuario>>()), Times.Once);
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
            var result = service.Create(entry);

            Assert.Multiple(() =>
            {
                repository.Verify(r => r.Create(It.IsAny<List<Usuario>>()), Times.Never);
                Assert.Zero(result);
            });
        }

        [Test]
        public void DeleteExample_ShouldReturn_NumberOfEntitiesDeleted()
        {
            var result = service.Delete(1);

            Assert.Multiple(() =>
            {
                repository.Verify(r => r.Delete(It.IsAny<long>()), Times.Once);
                Assert.NotZero(result);
            });
        }

        [Test]
        public void DeleteExample_WithInvalidId_ShouldReturn_Zero()
        {
            repository.Setup(x => x.Get(It.IsAny<long>())).Returns((Usuario)null);

            var result = service.Delete(3);

            Assert.Multiple(() =>
            {
                repository.Verify(r => r.Delete(It.IsAny<long>()), Times.Never);
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

            var result = service.Modify(entity);

            Assert.Multiple(() =>
            {
                repository.Verify(r => r.Modify(It.IsAny<Usuario>()), Times.Never);
                Assert.Null(result);
            });
        }

        [Test]
        public void ModifyExample_WithInvalidId_ShouldReturn_Null()
        {
            repository.Setup(x => x.Get(It.IsAny<long>())).Returns((Usuario)null);

            var entity = new Usuario
            {
                Id = 3,
                Name = "Mock Name",
                Description = "Mock Description"
            };

            var result = service.Modify(entity);

            Assert.Multiple(() =>
            {
                repository.Verify(r => r.Modify(It.IsAny<Usuario>()), Times.Never);
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

            var result = service.Modify(entity);

            Assert.Multiple(() =>
            {
                repository.Verify(r => r.Modify(It.IsAny<Usuario>()), Times.Once);
                Assert.NotNull(result);
            });
        }
    }
}
