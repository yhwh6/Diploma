using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Diploma.Models;

namespace Diploma
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DiplomaContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            services.AddControllersWithViews();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Accounts/Login";
                    options.AccessDeniedPath = "/Accounts/AccessDenied";
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Administrator", policy => policy.RequireRole("Administrator"));
                options.AddPolicy("Authorized", policy => policy.RequireAuthenticatedUser());
                options.AddPolicy("AccessRequests", policy => policy.RequireRole("Administrator"));
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "account",
                    pattern: "{controller=Accounts}/{action=Login}/{id?}");
                endpoints.MapControllerRoute(
                    name: "user",
                    pattern: "{controller=Users}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "contacts",
                    pattern: "{controller=Contacts}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "services",
                    pattern: "{controller=Services}/{action=Edit}/{id?}");
            });
        }
    }
}