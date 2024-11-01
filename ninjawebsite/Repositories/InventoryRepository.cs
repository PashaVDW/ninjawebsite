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

        public async Task<IEnumerable<Inventory>> GetAllInventoriesAsync()
        {
            return await _context.Inventories.ToListAsync();
        }

        public async Task<Inventory> GetInventoryByEquipmentAndNinjaIdAsync(int EqId, int ninjaId)
        {

            return await _context.Inventories.Where(i => i.EquipmentId == EqId && i.NinjaId == ninjaId).FirstOrDefaultAsync();
        }
        public async Task AddInventoryAsync(Inventory inventory)
        {
            _context.Inventories.Add(inventory);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteInventoryAsync(Inventory inventory)
        {
            _context.Inventories.Remove(inventory);
        }
    }
}
