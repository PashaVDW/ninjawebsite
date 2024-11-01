namespace ninjawebsite.Interfaces
{
    using ninjawebsite.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface INinjaRepository
    {
        Task<IEnumerable<Ninja>> GetAllNinjasAsync();
        Task<Ninja> CreateNinja(string name, decimal gold);
        Task<Ninja> GetNinjaByIdAsync(int id);
        Task UpdateNinja(Ninja ninja);
        Task DeleteNinjaAsync(int id);
    }
}
