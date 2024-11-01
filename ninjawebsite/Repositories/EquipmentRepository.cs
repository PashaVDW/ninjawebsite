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
        public async Task<Equipment> CreateEquipment(string name, int goldValue, int categoryId, int strength, int intelligence, int agility)
        {
            if (name.Length > 255 || goldValue > int.MaxValue || strength > int.MaxValue || intelligence > int.MaxValue || agility > int.MaxValue)
            {
                return null;
            }
            Equipment equipment = new Equipment()
            {
                Name = name,
                GoldValue = goldValue,
                CategoryId = categoryId,
                Strength = strength,
                Intelligence = intelligence,
                Agility = agility
            };
            _context.Equipments.Add(equipment);
            await _context.SaveChangesAsync();
            return equipment;
        }
        public async Task<Equipment> UpdateEquipment(Equipment equipment)
        {
            _context.Equipments.Update(equipment);
            await _context.SaveChangesAsync();
            return equipment;
        }
    }
}
