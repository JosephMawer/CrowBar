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
            var drinks = new Drinks[]
            {
                new Drinks() {Name = "Pepsi", Price = 2.50m, },
                new Drinks() {Name= "Coke",Price = 2.50m},
                new Drinks() {Name = "Fancy Drink", Description = "A really fancy fruity drink", Price = 6.00m,}
            };
            var sides = new Sides[]
            {
                // todo : figure out how to support options, via some dialog, + database
                new Sides() {Name = "Hot dog", Description = "Small / Large", Price = 2m},
                new Sides() {Name = "French Fries", Description= "Small / Large", Price = 5m, },
                new Sides() {Name = "Poutine", Description = "Yes, with cheese curds!", Price = 10m},
                new Sides() {Name = "Onion Rings", Description = "If you like it put a ring on it.", Price = 8m,}
            };
            var mains = new Mains[]
            {
                new Mains { Name = "All Beef Bacon Burger", Price = 17m,
                    ImageUrl = "img/pizzas/bacon.jpg", Description = "Fresh beef burger patty, bacon, cheese, lettuce, tomatoe, grilled onion."},
                new Mains {Name = "Crow Burger",Price = 17m,
                    Description = "Fresh beef burger patty, bacon, cheese, peanut butter, lettuce, tomatoe, grilled onion."},
                new Mains {Name = "Juicy Chicken Burger", Price = 17m, 
                    Description = "Grilled chicken breast, bacon, cheese, lettuce, tomatoe, mayo."},
                new Mains {Name = "Black Bean Burger", Price = 17m,
                    Description = "Delcious homemade black bean burger, lettuce, tomatoe, mayo"},
               
            };
  

            // extras
            // gravy can go in the extras category??

            AddRoles(db);


            var users = GetDefaultUsers();

            AddUsers(db, users);

            //orders.User = firstUser;
            //db.Orders.AddRange(orders);
            db.Drinks.AddRange(drinks);
            db.Sides.AddRange(sides);
            db.Mains.AddRange(mains);

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
                    else
                        userStore.AddToRoleAsync(user, "CUSTOMER").GetAwaiter().GetResult();
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

        static CrowBarUser firstUser = new CrowBarUser();


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

            firstUser = new CrowBarUser()
            {
                Email = "first@hotmail.com",
                NormalizedEmail = "FIRST@HOTMAIL.COM",
                UserName = "first@hotmail.com",
                NormalizedUserName = "FIRST@HOTMAIL.COM",
                PhoneNumber = "",
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            password = new PasswordHasher<CrowBarUser>();
            hashed = password.HashPassword(firstUser, "12345");
            firstUser.PasswordHash = hashed;
            return new List<CrowBarUser>() { admin, owner, firstUser };
        }
    }
}
