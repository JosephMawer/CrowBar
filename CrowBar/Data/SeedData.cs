using CrowBar.Areas.Identity.Data;
using CrowBar.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrowBar.Data
{
    public static class SeedData
    {
        public static void Initialize(CrowBarContext db,UserManager<CrowBarUser> userManager)
        {
            var drinks = new Drink[]
            {
                new Drink() {Name = "Pepsi", Price = 2.50m, },
                new Drink() {Name= "Coke",Price = 2.50m},
                new Drink() {Name = "Fancy Drink", Description = "A really fancy fruity drink", Price = 6.00m,}
            };
            var sides = new Side[]
            {
                new Side() {Name = "Fries", Description= "Regular Fries", Price = 2.50m, },
                new Side() {Name= "Sweet Potatoe Fries", Description = "The sweetest potatoe you know.", Price = 3.00m},
                new Side() {Name = "Salad", Description = "No one ever gets this..", Price = 3.00m,}
            };
            var mains = new Main[]
            {
                new Main { Name = "Chicken Burger", Price = 14.99m,
                    ImageUrl = "img/pizzas/bacon.jpg", Description = "Try our savoury fire roasted chicken tequilla with black peppermint and chipotle sauce with a side of delicisous oven roosted roosters!"},
                new Main {Name = "Bean Burger",Price = 12.99m,
                    Description = "Beans, beans, the magical fruit, the more you eat the more you toot. Healthy and delicious :)"},
                new Main {Name = "Poutine", Price = 8.99m, 
                    Description = "Try our mouth watering poutine and gravy with cheese from Empire!"},
                new Main {Name = "Salad", Price = 11.99m,
                    Description = "Salad... pweh!"},
                new Main {Name = "Hot Dog", Price = 6.99m, 
                    Description = "Get your delicious frankenfurt style sausage!"},
                new Main {Name = "Hamburger With Bacon", Price = 14.99m,
                    Description = "Try our savoury fire roasted chicken tequilla with black peppermint and chipotle sauce with a side of delicisous oven roosted roosters!"}
            };
            var orders = new Order()
            {
                OrderId = 1,
                Mains = mains.ToList()
            };

            db.Orders.AddRange(orders);
            db.Drinks.AddRange(drinks);
            db.Sides.AddRange(sides);
            db.Mains.AddRange(mains);



            AddRoles(db);


            var users = GetDefaultUsers();

            AddUsers(db, users);


           

            db.SaveChanges();
        }

        private static void AddUsers(CrowBarContext db, List<CrowBarUser> users)
        {
            foreach (var user in users)
            {
                if (!db.Users.Any(u => u.UserName == user.UserName))
                {
                    var userStore = new UserStore<CrowBarUser>(db);
                    userStore.CreateAsync(user).GetAwaiter().GetResult();
                    if (user.UserName.Contains("admin"))
                        userStore.AddToRoleAsync(user, "ADMINISTRATOR").GetAwaiter().GetResult();
                    else if (user.UserName.Contains("owner"))
                        userStore.AddToRoleAsync(user, "OWNER").GetAwaiter().GetResult();
                    //userManager.CreateAsync(user).GetAwaiter().GetResult();
                    //userManager.AddToRoleAsync(user, "OWNER").GetAwaiter().GetResult();
                }
            }
        }

        private static void AddRoles(CrowBarContext db)
        {
            string[] roles = new string[] { "Owner", "Administrator", "Customer" };

            foreach (string role in roles)
            {
                var roleStore = new RoleStore<IdentityRole>(db);

                if (!db.Roles.Any(r => r.Name == role))
                {
                    var identityRole = new IdentityRole(role)
                    {
                        NormalizedName = role.ToUpper()
                    };
                    roleStore.CreateAsync(identityRole);
                }
            }
        }

        private static List<CrowBarUser> GetDefaultUsers()
        {
            var admin = new CrowBarUser 
            {
                Email = "admin@hotmail.com",
                NormalizedEmail = "ADMIN@HOTMAIL.COM",
                UserName = "admin@hotmail.com",
                NormalizedUserName = "ADMIN@HOTMAIL.COM",
                PhoneNumber = "",
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            var password = new PasswordHasher<CrowBarUser>();
            var hashed = password.HashPassword(admin, "12345");
            admin.PasswordHash = hashed;

            var owner = new CrowBarUser 
            {
                Email = "owner@hotmail.com",
                NormalizedEmail = "OWNER@HOTMAIL.COM",
                UserName = "owner@hotmail.com",
                NormalizedUserName = "OWNER@HOTMAIL.COM",
                PhoneNumber = "",
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            password = new PasswordHasher<CrowBarUser>();
            hashed = password.HashPassword(owner, "12345");
            owner.PasswordHash = hashed;
            return new List<CrowBarUser>() { admin, owner };
        }
    }
}
