using ninjawebsite.Models;

namespace ninjawebsite.Interfaces
{
    public interface IShopRepository
    {
        Task<IEnumerable<Shop>> GetAllShops();
        Task<Shop> GetShopById(int id);
        Task AddShop(Shop shop);
        Task UpdateShop(Shop shop);
        Task CreateShopById(int ninjaId, int equipmentId);
        Task DeleteShop(Shop shop);
    }
}
