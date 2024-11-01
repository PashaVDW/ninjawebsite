using ninjawebsite.Models;

namespace ninjawebsite.Interfaces
{
    public interface IEquipmentRepository
    {
        Task<IEnumerable<Equipment>> GetAllEquipmentAsync();
        Task<Equipment> GetEquipmentByIdAsync(int id);

        Task<Equipment> CreateEquipment(string name, int goldValue, int categoryId, int strength, int intelligence, int agility);
    }
}
