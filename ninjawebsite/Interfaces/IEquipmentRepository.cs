using ninjawebsite.Models;

namespace ninjawebsite.Interfaces
{
    public interface IEquipmentRepository
    {
        Task<IEnumerable<Equipment>> GetAllEquipment();
        Task<Equipment> GetEquipmentById(int id);
        Task<Equipment> CreateEquipment(string name, int goldValue, int categoryId, int strength, int intelligence, int agility);
        Task<Equipment> UpdateEquipment(Equipment equipment);
        Task<Equipment> DeleteEquipment(Equipment equipment);
    }
}
