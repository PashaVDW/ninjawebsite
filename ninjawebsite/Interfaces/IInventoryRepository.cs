using ninjawebsite.Models;

namespace ninjawebsite.Interfaces
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<Inventory>> GetAllInventoriesAsync();
        Task<Inventory> GetInventoryByEquipmentAndNinjaIdAsync(int EqId, int ninjaId);
        Task AddInventoryAsync(Inventory inventory);
        Task DeleteInventoryAsync(Inventory inventory);


    }
}
