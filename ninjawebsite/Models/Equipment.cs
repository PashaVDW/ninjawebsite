namespace ninjawebsite.Models
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal GoldValue { get; set; }
        public Category Category { get; set; }
        public int Strength { get; set; }
        public int Intelligence { get; set; }
        public int Agility { get; set; }
        public ICollection<Inventory> Inventory { get; set; } = new List<Inventory>();
    }
    public enum Category
    {
        Head,
        Chest,
        Hand,
        Feet,
        Ring,
        Necklace
    }
}
