namespace ninjawebsite.Interfaces
{
    using ninjawebsite.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface INinjaRepository
    {
        Task<IEnumerable<Ninja>> GetAllNinjasAsync();
        Task<Ninja> CreateNinja(string name, decimal gold);
        Task<Ninja> GetNinjaById(int id);
        Task UpdateNinja(Ninja ninja);
        Task DeleteNinjaAsync(int id);
        Equipment GetEquipmentForNinja(int id, int categoryId);
        Task DeleteAllEquipmentForNinja(int ninjaId);
        Equipment GetHeadEquipmentForNinja(int id) => GetEquipmentForNinja(id, 1);
        Equipment GetChestEquipmentForNinja(int id) => GetEquipmentForNinja(id, 2);
        Equipment GetHandEquipmentForNinja(int id) => GetEquipmentForNinja(id, 3);
        Equipment GetFeetEquipmentForNinja(int id) => GetEquipmentForNinja(id, 4);
        Equipment GetRingEquipmentForNinja(int id) => GetEquipmentForNinja(id, 5);
        Equipment GetNecklaceEquipmentForNinja(int id) => GetEquipmentForNinja(id, 6);
    }
}
