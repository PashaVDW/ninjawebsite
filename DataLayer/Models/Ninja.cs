namespace ninjawebsite.Models
{
    using System.Collections.Generic;
    public class Ninja
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Gold { get; set; }
        public ICollection<Inventory> Inventory { get; set; } = new List<Inventory>();
    }
}
