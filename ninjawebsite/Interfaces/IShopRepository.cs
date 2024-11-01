using ninjawebsite.Models;

namespace ninjawebsite.Interfaces
{
    public interface IShopRepository
    {
        Task<IEnumerable<Shop>> GetAllShopsAsync();
        Task<Shop> GetShopByIdAsync(int id);
        Task AddShopAsync(Shop shop);
        Task UpdateShopAsync(Shop shop);
        Task CreateShopById(int ninjaId, int equipmentId);
    }
}
