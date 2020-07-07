using System.Collections.Generic;

namespace CrowBar.Models
{
    public class MenuItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string GetFormattedBasePrice()
        {
            return "9.99";
        }
        public List<SideMenuItem> Sides = new List<SideMenuItem>();
    }
}
