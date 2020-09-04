using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using MTG_Mvc.DBContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MTG_Mvc.Domain.Entities;
using MTG_Mvc.Services;
using MTG_Mvc.Repositories;

namespace MTG_Mvc
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
            services.AddRazorPages();
            services.AddControllers();
            services.AddTransient<JsonFileProductService>();

            //services.AddDbContext<DbContext>(options =>
            //options.UseSqlServer(@"Server=.;Database=MTG_Mvc;User=sa;Password=change_this_password;"));
            services.AddDbContext<SqlDbContext>();
            services.AddScoped<decklist>();
            services.AddScoped<decklistService>(); 
            services.AddScoped<decklistRepository>();
            services.AddScoped(typeof(IdecklistServiceInterface), typeof(decklistService));
            services.AddScoped(typeof(IdecklistRepositoryInterface), typeof(decklistRepository));
           
            services.AddMvc();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                //endpoints.MapGet("/products", (context) =>
                //{
                //    var products = app.ApplicationServices.GetService<JsonFileProductService>().GetProducts();
                //    var json = JsonSerializer.Serialize<IEnumerable<product>>(products);
                //    return context.Response.WriteAsync(json);
                //});
            });
        }
    }
}
