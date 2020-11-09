using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using TeachMe.API;
using TeachMe.API.Extensions;
using TeachMe.API.Filters;
using TeachMe.Core.Resources;
using TeachMe.Repository.Context;

namespace TeachMe
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddFluentValidation(fvc =>
                {
                    fvc.RegisterValidatorsFromAssembly(Assembly.Load("TeachMe.API"));
                    fvc.ImplicitlyValidateChildProperties = true;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                    {
                        var assemblyName = new AssemblyName(typeof(Resource).GetTypeInfo().Assembly.FullName);
                        return factory.Create("Resource", assemblyName.Name);
                    };
                });

            services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("pt-BR"),
                    };

                    options.DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR");
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;

                    options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
                });

            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddMvc(config => config.Filters.Add(typeof(ExceptionFilter)));

            services.AddControllers();
            services.AddCors();

            services.Configure<DbOptions>(Configuration.GetSection("DbOptions"));
            services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly });
            services.AddHealthChecks().AddCheck<CustomHealthCheck>("applicationHealth", tags: new[] { "app_tag" });
            services.AddDbContext<TeachDbContext>(opt => opt.UseMySql(Configuration.GetConnectionString("DevConnection")));

            services.ConfigureServiceSwagger();
            services.ConfigureServiceAuthentication();
            services.ConfigureServiceDependencyInjection();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var localization = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(localization.Value);

            app.UseAuthorization();
            app.UseAuthentication();

            app.ConfigureHealthCheck();

            app.ConfigureAppSwagger();

            //app.UseHttpsRedirection();

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
