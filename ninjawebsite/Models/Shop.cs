namespace ninjawebsite.Models
{
    public class Shop
    {
        public int Id { get; set; }
        public int NinjaId { get; set; }
        public Ninja Ninja { get; set; }

        public int EquipmentId { get; set; }
        public Equipment Equipment { get; set; }

        public bool IsBeschikbaar { get; set; }
    }
}
