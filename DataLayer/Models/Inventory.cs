namespace ninjawebsite.Models
{
    public class Inventory
    {
        public int NinjaId { get; set; }
        public Ninja Ninja { get; set; }
        public int EquipmentId { get; set; }
        public Equipment Equipment { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
