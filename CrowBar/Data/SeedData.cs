using CrowBar.Models;
using System.Linq;

namespace CrowBar.Data
{
    public static class SeedData
    {
        public static void Initialize(ApplicationDbContext db)
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

            db.SaveChanges();
        }
   
    }
}
