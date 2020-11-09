using Microsoft.Extensions.DependencyInjection;
using TeachMe.Core.Resources;
using TeachMe.Repository.Context;
using TeachMe.Repository.Repositories;
using TeachMe.Repository.Repositories.Interfaces;
using TeachMe.Service.Services;
using TeachMe.Service.Services.Interfaces;

namespace TeachMe.API.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static void ConfigureServiceDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<TeachDbContext>();
            services.AddTransient<IResourceLocalizer, ResourceLocalizer>();
            services.AddTransient<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddTransient<IUsuarioServico, UsuarioServico>();
            services.AddTransient<ICargoRepositorio, CargoRepositorio>();
            services.AddTransient<ICargoServico, CargoServico>();
            services.AddTransient<IDisciplinaRepositorio, DisciplinaRepositorio>();
            services.AddTransient<IDisciplinaServico, DisciplinaServico>();
            services.AddTransient<IProfessorRepositorio, ProfessorRepositorio>();
            services.AddTransient<IProfessorServico, ProfessorServico>();
            services.AddTransient<IEmailRepositorio, EmailRepositorio>();
            services.AddTransient<IValidacaoRepositorio, ValidacaoRepositorio>();
            services.AddTransient<IAulaRepositorio, AulaRepositorio>();
            services.AddTransient<IAulaServico, AulaServico>();
            services.AddTransient<IEscolaridadeRepositorio, EscolaridadeRepositorio>();
            services.AddTransient<IEscolaridadeServico, EscolaridadeServico>();
            services.AddTransient<IModalidadeEnsinoRepositorio, ModalidadeEnsinoRepositorio>();
            services.AddTransient<IModalidadeEnsinoServico, ModalidadeEnsinoServico>();
        }
    }
}
