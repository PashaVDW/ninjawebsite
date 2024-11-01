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

        public async Task UpdateNinja(Ninja ninja)
        {
            _context.Ninjas.Update(ninja);
            await _context.SaveChangesAsync();
        }

        public async Task<Ninja> CreateNinja(string name, decimal gold)
        {
            if (name.Length > 255 || gold > int.MaxValue)
            {
                return null;
            }

            Ninja ninja = new Ninja()
            {
                Name = name,
                Gold = gold
            };

            _context.Ninjas.Add(ninja);
            await _context.SaveChangesAsync();

            return ninja;
        }
        public async Task DeleteNinjaAsync(int id)
        {
            var ninja = await _context.Ninjas
                .Include(n => n.Inventory)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (ninja != null)
            {
                _context.Inventories.RemoveRange(ninja.Inventory);

                _context.Ninjas.Remove(ninja);
                await _context.SaveChangesAsync();
            }
        }
    }

}
