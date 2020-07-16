using CrowBar.Areas.Identity.Data;
using System;
using System.Collections.Generic;

namespace CrowBar.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public CrowBarUser User { get; set; }
        public DateTime CreatedTime { get; set; }
        public string OrderStatus { get; set; } = "Preparing";

        public string GetFormattedBasePrice()
        {
            var total = 0m;
            foreach (var item in OrderItems)
                total += item.Quantity * item.MenuItem.Price;
            return total.ToString("F2");
        }
       
    }
}
