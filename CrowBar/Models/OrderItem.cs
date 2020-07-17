namespace CrowBar.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public MenuItem MenuItem { get; set; }
        public int Quantity { get; set; } = 1;  // default value
    }
}
