namespace ninjawebsite.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using ninjawebsite.Interfaces;
    using ninjawebsite.Models;

    public class NinjaRepository : INinjaRepository
    {
        private readonly NinjaContext _context;

        public NinjaRepository(NinjaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ninja>> GetAllNinjasAsync()
        {
            return await _context.Ninjas.ToListAsync();
        }

        public async Task<Ninja> GetNinjaByIdAsync(int id)
        {
            return await _context.Ninjas.FindAsync(id);
        }
        public async Task UpdateNinjaAsync(Ninja ninja)
        {
            _context.Ninjas.Update(ninja);
            await _context.SaveChangesAsync();
        }
    }

}
