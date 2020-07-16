using System.Collections.Generic;

namespace CrowBar.Models
{
    public abstract class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
    public class Mains : MenuItem
    {
        

      
        public string ImageUrl { get; set; }

        
        // a main comes with a side and a drink
        //public Side Sides { get; set; }
        //public Drink Drink { get; set; }

        public string Note { get; set; }

        /// <summary>
        /// The total cost of the menu item, including cost of sides
        /// </summary>
        public string Total(){
            var total = 0m;

            //if (Sides != null) total += Sides.Price;
            //if (Drink != null) total += Drink.Price;
            total += this.Price;
            return total.ToString("F2");
        }
        

    }
}
