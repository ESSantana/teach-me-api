using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.Reflection;

namespace TeachMe.API.Extensions
{
    public static class SwaggerExtension
    {
        public static void ConfigureServiceSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen().AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "TeachMe API",
                    Description = "Api criada para a disciplina de Laboratório de software da Universidade Salvador",
                    Contact = new OpenApiContact
                    {
                        Name = "Emerson Santana",
                        Email = "029171058@unifacs.edu.br",
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Usar de acordo com a Licença CreativeCommons",
                        Url = new Uri("https://creativecommons.org/licenses/by-nc/4.0/legalcode"),
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public static void ConfigureAppSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger().UseSwaggerUI(c =>
            {
                c.DocExpansion(DocExpansion.None);
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TeachMe API");
            });
        }
    }
}
