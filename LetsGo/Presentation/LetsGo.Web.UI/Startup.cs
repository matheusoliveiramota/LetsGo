using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using LetsGo.Web.UI.Services;
using LetsGo.Web.UI.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LetsGo.Web.UI
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
            services.AddControllersWithViews();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
            {
                //forma de autenticacao local do usuario
                options.DefaultScheme = "Cookies";
                //protocolo que define o fluxo de autenticao
                options.DefaultChallengeScheme = "OpenIdConnect";
            })
            .AddCookie()
            .AddOpenIdConnect(options =>
            {
                options.SignInScheme = "Cookies";
                options.Authority = Configuration.GetSection("UrlIdentity").Value;
                options.ClientId = "LetsGo.MVC";
                options.ClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0";
                options.SaveTokens = true;
                //1) autorizacao e 2) identidade do usuario
                options.ResponseType = "id_token token";
                //c�digo de autorizacao + token de identidade
                options.RequireHttpsMetadata = false;
                options.GetClaimsFromUserInfoEndpoint = true;
            });

            services.AddScoped<IUsuarioServiceUI, UsuarioServiceUI>(x =>
                new UsuarioServiceUI(Configuration)
            );

            services.AddScoped<IRestauranteServiceUI, RestauranteServiceUI>(x =>
                new RestauranteServiceUI(Configuration)
            );

            services.AddScoped<IPlacaServiceUI, PlacaServiceUI>(x =>
                new PlacaServiceUI(Configuration)
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
