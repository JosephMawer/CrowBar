using System.Collections.Generic;

namespace CrowBar.Models
{
    public class Order
    {
        public string GetFormattedBasePrice()
        {
            var total = 0f;
            foreach (var item in Items)
                total += float.Parse(item.Total());
            return total.ToString("F2");
        }
        public List<MenuItem> Items { get; set; } = new List<MenuItem>();
        public List<SideMenuItem> Sides { get; set; } = new List<SideMenuItem>();
    }
}
