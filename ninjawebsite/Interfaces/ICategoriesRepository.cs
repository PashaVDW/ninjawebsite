using ninjawebsite.Models;

namespace ninjawebsite.Interfaces
{
    public interface ICategoriesRepository
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();

    }
}
