using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrowBar.Areas.Identity.Data;
using CrowBar.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CrowBar.Data
{
    public class CrowBarContext : IdentityDbContext<CrowBarUser>
    {
        public CrowBarContext(DbContextOptions<CrowBarContext> options)
            : base(options)
        {
        }

        public DbSet<Drinks> Drinks { get; set; }
        public DbSet<Sides> Sides { get; set; }
        public DbSet<Mains> Mains { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<Order>().HasMany(x => x.OrderItems);
            //builder.Entity<MenuItem>().HasNoKey();
        }
    }
}
