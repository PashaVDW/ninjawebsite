using ninjawebsite.Models;

namespace ninjawebsite.Interfaces
{
    public interface IShopRepository
    {
        Task<IEnumerable<Shop>> GetAllShopsAsync();
    }
}
