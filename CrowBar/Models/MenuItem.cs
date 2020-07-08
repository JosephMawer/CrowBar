using System.Collections.Generic;

namespace CrowBar.Models
{
    public class MenuItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        // the price of a the menu item
        public string Price {get;set;}

        // the total cost of the menu item, including cost of sides
        public string Total(){
            var total = 0f;
            foreach (var side in Sides){
                total += float.Parse(side.Price);
            }
            total += float.Parse(this.Price);
            return total.ToString("F2");
        }
        public List<SideMenuItem> Sides = new List<SideMenuItem>();
    }
}
