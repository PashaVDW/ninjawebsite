namespace ninjawebsite.ViewModels
{
    public class ShopsViewModel
    {
        public int Id { get; set; }
        public int EqId { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public decimal Gold { get; set; }
        public bool IsAvailable { get; set; }
        public int CategoryId { get; set; }
    }
}
