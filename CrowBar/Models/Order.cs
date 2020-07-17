using CrowBar.Areas.Identity.Data;
using Microsoft.AspNetCore.Razor.Language.Extensions;
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

        public int OrderCount()
        {
            var count = 0;
            foreach (var item in OrderItems)
                count += item.Quantity;
            return count;
        }
        public string GetFormattedBasePrice()
        {
            var total = 0m;
            foreach (var item in OrderItems)
                total += item.Quantity * item.MenuItem.Price;
            return total.ToString("F2");
        }
       
    }
}
