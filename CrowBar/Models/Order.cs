using System;
using System.Collections.Generic;

namespace CrowBar.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public int MainId { get; set; }
        public int SideId { get; set; }


        public string UserId { get; set; }

        public DateTime CreatedTime { get; set; }

        public List<Main> Mains { get; set; } = new List<Main>();
        public List<Side> Sides { get; set; } = new List<Side>();

        public string GetFormattedBasePrice()
        {
            var total = 0m;
            foreach (var main in Mains)
                total += decimal.Parse(main.Total());
            return total.ToString("F2");
        }
       
    }
}
