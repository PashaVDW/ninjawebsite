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

        public async Task<Inventory> GetInventoryByIdAsync(int id)
        {
            return await _context.Inventories.FindAsync(id);
        }
        public async Task AddInvertoryAsync(Inventory inventory)
        {
            _context.Inventories.Add(inventory);
            await _context.SaveChangesAsync();
        }
    }
}
