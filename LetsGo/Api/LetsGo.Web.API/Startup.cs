using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LetsGo.Domain.Interfaces;
using LetsGo.Web.API.Data.SQL;
using LetsGo.Web.API.Data.SQL.Repositories;
using LetsGo.Web.API.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Unity.Injection;

namespace LetsGo.Web.API
{
    public class Startup
    {
        public Startup(Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddScoped<IUsuarioRepository, UsuarioRepository>(x =>
                new UsuarioRepository(
                    new DataConnection(Configuration)
                )
            );
            services.AddScoped<IRestauranteRepository, RestauranteRepository>(x =>
                new RestauranteRepository(
                    new DataConnection(Configuration)
                )
            );
            services.AddScoped<IPlacaRepository, PlacaRepository>(x =>
                new PlacaRepository(
                    new DataConnection(Configuration)
                )
            );
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IRestauranteService, RestauranteService>();
            services.AddScoped<IPlacaService, PlacaService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
