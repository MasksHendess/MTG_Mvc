using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MTG_Mvc.APIControllers;
using MTG_Mvc.DBContext;
using MTG_Mvc.Domain.Entities;
using MTG_Mvc.Repositories;
using MTG_Mvc.Services;

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
            services.AddMemoryCache();
            services.AddRazorPages();
            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddTransient<JsonFileProductService>();
            services.AddDbContext<SqlDbContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<decklist>();
            services.AddScoped<decklistService>(); 
            services.AddScoped<decklistRepository>();
            services.AddScoped<mtgioAPIController>();

            services.AddScoped<cardService>();
            services.AddScoped<cardRepository>();

            services.AddScoped(typeof(IdecklistServiceInterface), typeof(decklistService));
            services.AddScoped(typeof(IdecklistRepositoryInterface), typeof(decklistRepository));
            services.AddScoped(typeof(IcardRepositoryInterface), typeof(cardRepository));

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
