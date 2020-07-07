using System.Collections.Generic;

namespace CrowBar.Models
{
    public class Sides
    {
        public List<SideMenuItem> SideItems { get; set; }
        public Sides()
        {
            SideItems = new List<SideMenuItem>()
            {
                new SideMenuItem() {Name = "Fries", Description= "Regular Fries", Price = "2.50"},
                new SideMenuItem() {Name= "Sweet Potatoe Fries", Description = "The sweetest potatoe you know.", Price = "3.00"},
                new SideMenuItem() {Name = "Salad", Description = "No one ever gets this..", Price = "3.00"}
            };
        }
    }
}
