using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CrowBar.Areas.Identity;
using CrowBar.Data;
using CrowBar.Areas.Identity.Data;

namespace CrowBar
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(
            //        Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<CrowBarContext>(
                options => options.UseSqlite("Data Source=CrowBar.db"));

            //services.AddIdentity<IdentityUser, IdentityRole<Guid>>(options =>
            //    {
            //        // make it really easy to create a password :)
            //        options.Password.RequireDigit = false;
            //        options.Password.RequiredLength = 6;
            //        options.Password.RequireLowercase = false;
            //        options.Password.RequireNonAlphanumeric = false;
            //        options.Password.RequireUppercase = false;


            //        options.User.RequireUniqueEmail = false;

            //        options.SignIn.RequireConfirmedAccount = false;
            //        options.SignIn.RequireConfirmedEmail = false;
            //        options.SignIn.RequireConfirmedPhoneNumber = false;

            //        options.Lockout.MaxFailedAccessAttempts = 6;
            //    })
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultUI()
            //    .AddDefaultTokenProviders();



            services.AddDefaultIdentity<CrowBarUser>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<CrowBarContext>();


            services.Configure<IdentityOptions>(options =>
            {
                // make it really easy to create a password :)
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;


                options.User.RequireUniqueEmail = false;

                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                options.Lockout.MaxFailedAccessAttempts = 6;
            });

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<CrowBarUser>>();
            services.AddHttpContextAccessor();
            SeedData(services);
        }

        private void SeedData(IServiceCollection service)
        {
            var services = service.BuildServiceProvider();
            // Initialize the database
            var scopeFactory = services.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<CrowBarContext>();
                var userManager = services.GetService<UserManager<CrowBarUser>>();
                if (db.Database.EnsureCreated())
                {
                    CrowBar.Data.SeedData.Initialize(db, userManager);
                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // automatically map all the controllers in the Identity.UI package
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
