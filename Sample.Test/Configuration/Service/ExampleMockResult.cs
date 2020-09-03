using TeachMe.Core.Entities;
using System.Collections.Generic;

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
                    Name = "Example Mock Name 1",
                    Description = "Example Mock Description 1"
                },
                new Usuario
                {
                    Id = 2,
                    Name = "Example Mock Name 2",
                    Description = "Example Mock Description 2"
                },
                new Usuario
                {
                    Id = 3,
                    Name = "Example Mock Name 3",
                    Description = "Example Mock Description 3"
                }
            };
        }
    }
}
