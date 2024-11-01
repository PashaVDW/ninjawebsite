﻿namespace ninjawebsite.ViewModels
{
    public class EquipmentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal GoldValue { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int Strength { get; set; }
        public int Intelligence { get; set; }
        public int Agility { get; set; }
        public bool AddToShop { get; set; }

    }
}