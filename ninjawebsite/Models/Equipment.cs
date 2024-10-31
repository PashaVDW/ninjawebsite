namespace ninjawebsite.Models
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public decimal WaardeGoud { get; set; }
        public Categorie Categorie { get; set; }
        public int Strength { get; set; }
        public int Intelligence { get; set; }
        public int Agility { get; set; }

        public ICollection<Inventory> Inventory { get; set; } = new List<Inventory>();
    }

    public enum Categorie
    {
        Head,
        Chest,
        Hand,
        Feet,
        Ring,
        Necklace
    }
}
