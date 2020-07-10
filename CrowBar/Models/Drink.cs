namespace CrowBar.Models
{
    public class Drink : IMenuItem
    {
        public int DrinkId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
