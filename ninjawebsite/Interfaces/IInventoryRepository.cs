using ninjawebsite.Models;

namespace ninjawebsite.Interfaces
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<Inventory>> GetAllInventories();
        Task<Inventory> GetInventoryByEquipmentAndNinjaId(int EqId, int ninjaId);
        Task AddInventory(Inventory inventory);
        Task DeleteInventory(Inventory inventory);


    }
}
