using Microsoft.EntityFrameworkCore;
using ninjawebsite.Interfaces;
using ninjawebsite.Models;

namespace ninjawebsite.Repositories
{
    public class ShopRepository : IShopRepository
    {
        private readonly NinjaContext _context;
        public ShopRepository(NinjaContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Shop>> GetAllShopsAsync()
        {
            return await _context.Shops.ToListAsync();
        }
    }
}
