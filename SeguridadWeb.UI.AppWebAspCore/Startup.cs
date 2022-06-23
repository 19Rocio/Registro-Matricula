using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// ************************************************
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

using Microsoft.EntityFrameworkCore;
using SeguridadWeb.AccesoADatos;
using System.Net.Http;

namespace SeguridadWeb.UI.AppWebAspCore
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
            services.AddControllersWithViews().AddRazorRuntimeCompilation();//Actualizar cambios en tiempo de ejecucuión
            services.AddControllersWithViews();
            // Configurar la autentificacion 
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie((o) =>
            {
                o.LoginPath = new PathString("/Usuario/login");
                o.Events.OnRedirectToAccessDenied = context =>
                {
                    context.Response.Redirect("/Home/Index");
                    return Task.CompletedTask;
                };
                o.ExpireTimeSpan = TimeSpan.FromHours(8);
                o.SlidingExpiration = true;
            });
            //Agregar el Client HttpClient para consumir api
            //services.AddTransient(sp => new HttpClient
            //{
            //    BaseAddress = new Uri("http://www.EscuelaElCapulinRegistro.somee.com")
            //});
            //-----------------------------------------------------
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

            app.UseAuthorization();
            //********************************
            // Permitir la autentificacion en la aplicacion Web
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                    endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

           //Ruta del pack para generar los pdf de las vistas
            Rotativa.AspNetCore.RotativaConfiguration.Setup(env.WebRootPath, "../Rotativa/Windows");
        }
    }
}
