using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Services;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace SalesWebMvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<SalesWebMvcContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SalesWebMvcContext") ?? throw new InvalidOperationException("Connection string 'SalesWebMvcContext' not found.")));
            
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<SeedingService>();
            builder.Services.AddScoped<SellerService>(); 
            builder.Services.AddScoped<DepartmentService>();




            var app = builder.Build();

            var eNus = new CultureInfo("en-US");
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(eNus),
                SupportedCultures = new List<CultureInfo> { eNus },
                SupportedUICultures = new List<CultureInfo> { eNus }
            };

            app.UseRequestLocalization(localizationOptions);
                
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.Services.CreateScope().ServiceProvider.GetRequiredService<SeedingService>().Seed();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}