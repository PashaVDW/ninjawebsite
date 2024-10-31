namespace ninjawebsite.Models
{
    using System.Collections.Generic;

    public class Ninja
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public decimal Goud { get; set; }

        public ICollection<Inventory> Inventory { get; set; } = new List<Inventory>();
    }
}
