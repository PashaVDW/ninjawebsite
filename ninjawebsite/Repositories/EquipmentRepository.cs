using Microsoft.EntityFrameworkCore;
using ninjawebsite.Interfaces;
using ninjawebsite.Models;

namespace ninjawebsite.Repositories
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly NinjaContext _context;
        public EquipmentRepository(NinjaContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Equipment>> GetAllEquipmentAsync()
        {
            return await _context.Equipments.ToListAsync();
        }
        public async Task<Equipment> GetEquipmentByIdAsync(int id)
        {
            return await _context.Equipments.FindAsync(id);
        }
    }
}
