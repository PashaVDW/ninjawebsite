namespace ninjawebsite.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using ninjawebsite.Interfaces;
    using ninjawebsite.Models;

    public class NinjaRepository : INinjaRepository
    {
        private readonly NinjaContext _context;

        public NinjaRepository(NinjaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ninja>> GetAllNinjas()
        {
            var ninjas = await _context.Ninjas.ToListAsync();
            List<Ninja> ninjaList = new List<Ninja>();
            foreach (var ninja in ninjas)
            {
                ninjaList.Add(ninja);
            }
            return ninjaList;
        }

        public async Task<Ninja> GetNinjaById(int id)
        {
            var ninjas = await _context.Ninjas.ToListAsync();
            foreach (var ninja in ninjas)
            {
                if (ninja.Id == id)
                {
                    return ninja;
                }
            }
            return null;
        }

        public async Task UpdateNinja(Ninja ninja)
        {
            _context.Ninjas.Update(ninja);
            await _context.SaveChangesAsync();
        }

        public async Task<Ninja> CreateNinja(string name, decimal gold)
        {
            if (name.Length > 255 || gold > int.MaxValue)
            {
                return null;
            }

            Ninja ninja = new Ninja
            {
                Name = name,
                Gold = gold
            };

            _context.Ninjas.Add(ninja);
            await _context.SaveChangesAsync();

            return ninja;
        }

        public async Task DeleteNinja(int id)
        {
            var ninjas = await _context.Ninjas.Include(n => n.Inventory).ToListAsync();
            Ninja ninjaToDelete = null;

            foreach (var ninja in ninjas)
            {
                if (ninja.Id == id)
                {
                    ninjaToDelete = ninja;
                    break;
                }
            }

            if (ninjaToDelete != null)
            {
                foreach (var inventoryItem in ninjaToDelete.Inventory)
                {
                    _context.Inventories.Remove(inventoryItem);
                }

                _context.Ninjas.Remove(ninjaToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public Equipment GetEquipmentForNinja(int id, int categoryId)
        {
            var inventories = _context.Inventories.ToList();
            var equipments = _context.Equipments.ToList();

            foreach (var inventory in inventories)
            {
                if (inventory.NinjaId == id)
                {
                    foreach (var equipment in equipments)
                    {
                        if (equipment.Id == inventory.EquipmentId && equipment.CategoryId == categoryId)
                        {
                            return equipment;
                        }
                    }
                }
            }
            return null;
        }

        public async Task DeleteAllEquipmentForNinja(int ninjaId)
        {
            var inventories = await _context.Inventories.Include(i => i.Equipment).ToListAsync();
            List<Inventory> ninjaInventory = new List<Inventory>();
            decimal totalGoldValue = 0;

            foreach (var inventory in inventories)
            {
                if (inventory.NinjaId == ninjaId)
                {
                    ninjaInventory.Add(inventory);
                    if (inventory.Equipment != null)
                    {
                        totalGoldValue += inventory.Equipment.GoldValue;
                    }
                }
            }

            if (ninjaInventory.Count > 0)
            {
                var ninja = await _context.Ninjas.FindAsync(ninjaId);
                if (ninja != null)
                {
                    ninja.Gold += totalGoldValue;
                }

                foreach (var inventoryItem in ninjaInventory)
                {
                    _context.Inventories.Remove(inventoryItem);
                }

                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Equipment>> GetAllEquipmentForNinja(int ninjaId)
        {
            var inventories = await _context.Inventories.ToListAsync();
            var equipments = await _context.Equipments.ToListAsync();
            List<Equipment> ninjaEquipments = new List<Equipment>();

            foreach (var inventory in inventories)
            {
                if (inventory.NinjaId == ninjaId)
                {
                    foreach (var equipment in equipments)
                    {
                        if (equipment.Id == inventory.EquipmentId)
                        {
                            ninjaEquipments.Add(equipment);
                        }
                    }
                }
            }

            return ninjaEquipments;
        }

        public Equipment GetHeadEquipmentForNinja(int id) => GetEquipmentForNinja(id, 1);
        public Equipment GetChestEquipmentForNinja(int id) => GetEquipmentForNinja(id, 2);
        public Equipment GetHandEquipmentForNinja(int id) => GetEquipmentForNinja(id, 3);
        public Equipment GetFeetEquipmentForNinja(int id) => GetEquipmentForNinja(id, 4);
        public Equipment GetRingEquipmentForNinja(int id) => GetEquipmentForNinja(id, 5);
        public Equipment GetNecklaceEquipmentForNinja(int id) => GetEquipmentForNinja(id, 6);
    }
}
