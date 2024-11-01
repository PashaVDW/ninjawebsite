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

        public async Task<Ninja> GetNinjaById(int id)
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

        public Equipment GetEquipmentForNinja(int id, int categoryId)
        {
            var ninja = GetNinjaById(id);

            return (from i in _context.Inventories
                    join e in _context.Equipments on i.EquipmentId equals e.Id
                    where i.NinjaId == id && e.CategoryId == categoryId
                    select e).FirstOrDefault();
        }

        public async Task DeleteAllEquipmentForNinja(int ninjaId)
        {
            var ninjaInventory = await _context.Inventories
                .Where(i => i.NinjaId == ninjaId)
                .ToListAsync();

            if (ninjaInventory.Any())
            {
                _context.Inventories.RemoveRange(ninjaInventory);
                await _context.SaveChangesAsync();
            }
        }

        public Equipment GetHeadEquipmentForNinja(int id) => GetEquipmentForNinja(id, 1);
        public Equipment GetChestEquipmentForNinja(int id) => GetEquipmentForNinja(id, 2);
        public Equipment GetHandEquipmentForNinja(int id) => GetEquipmentForNinja(id, 3);
        public Equipment GetFeetEquipmentForNinja(int id) => GetEquipmentForNinja(id, 4);
        public Equipment GetRingEquipmentForNinja(int id) => GetEquipmentForNinja(id, 5);
        public Equipment GetNecklaceEquipmentForNinja(int id) => GetEquipmentForNinja(id, 6);

    }
}
