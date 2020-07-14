//using System;
//using CrowBar.Areas.Identity.Data;
//using CrowBar.Data;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.UI;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;

//[assembly: HostingStartup(typeof(CrowBar.Areas.Identity.IdentityHostingStartup))]
//namespace CrowBar.Areas.Identity
//{
//    public class IdentityHostingStartup : IHostingStartup
//    {
//        public void Configure(IWebHostBuilder builder)
//        {
//            builder.ConfigureServices((context, services) =>
//            {
//                services.AddDbContext<CrowBarContext>(options =>
//                    options.UseSqlite(
//                        context.Configuration.GetConnectionString("CrowBarContextConnection")));

//                services.AddDefaultIdentity<CrowBarUser>(options => options.SignIn.RequireConfirmedAccount = true)
//                    .AddEntityFrameworkStores<CrowBarContext>();
//            });
//        }
//    }
//}