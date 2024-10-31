using ninjawebsite.Models;

namespace ninjawebsite.Interfaces
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<Inventory>> GetAllInventoriesAsync();
        Task<Inventory> GetInventoryByIdAsync(int id);
        Task AddInvertoryAsync(Inventory inventory);
    }
}
