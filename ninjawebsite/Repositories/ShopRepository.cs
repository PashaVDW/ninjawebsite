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
        public async Task<IEnumerable<Shop>> GetAllShops()
        {
            return await _context.Shops.ToListAsync();
        }
        public async Task<Shop> GetShopById(int id)
        {
            return await _context.Shops.FindAsync(id);
        }
        public async Task AddShop(Shop shop)
        {
            _context.Shops.Add(shop);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateShop(Shop shop)
        {
            _context.Shops.Update(shop);
            await _context.SaveChangesAsync();
        }
        public async Task CreateShopById(int ninjaId, int equipmentId)
        {
            var shop = new Shop
            {
                NinjaId = ninjaId,
                EquipmentId = equipmentId,
                IsAvailable = true
            };
            await AddShop(shop);
        }
        public async Task DeleteShop(Shop shop)
        {
            _context.Shops.Remove(shop);
            await _context.SaveChangesAsync();
        }
    }
}
