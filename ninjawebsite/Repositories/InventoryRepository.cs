using Microsoft.EntityFrameworkCore;
using ninjawebsite.Interfaces;
using ninjawebsite.Models;

namespace ninjawebsite.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly NinjaContext _context;

        public InventoryRepository(NinjaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Inventory>> GetAllInventories()
        {
            return await _context.Inventories.ToListAsync();
        }

        public async Task<Inventory> GetInventoryByEquipmentAndNinjaId(int EqId, int ninjaId)
        {

            return await _context.Inventories.Where(i => i.EquipmentId == EqId && i.NinjaId == ninjaId).FirstOrDefaultAsync();
        }
        public async Task AddInventory(Inventory inventory)
        {
            _context.Inventories.Add(inventory);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteInventory(Inventory inventory)
        {
            _context.Inventories.Remove(inventory);
        }
    }
}
