using Microsoft.EntityFrameworkCore;
using ninjawebsite.Interfaces;
using ninjawebsite.Models;

namespace ninjawebsite.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly NinjaContext _context;

        public CategoriesRepository(NinjaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
