using Microsoft.EntityFrameworkCore;
using ninjawebsite.Models;

public class NinjaContext : DbContext
{
    public NinjaContext(DbContextOptions<NinjaContext> options) : base(options)
    {
    }
    public DbSet<Ninja> Ninjas { get; set; }
    public DbSet<Equipment> Equipments { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<Shop> Shops { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Inventory>()
            .HasKey(i => new { i.NinjaId, i.EquipmentId });

        modelBuilder.Entity<Inventory>()
            .HasOne(i => i.Ninja)
            .WithMany(n => n.Inventory)
            .HasForeignKey(i => i.NinjaId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Inventory>()
           .HasOne(i => i.Equipment)
           .WithMany(e => e.Inventory)
           .HasForeignKey(i => i.EquipmentId)
           .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Inventory>()
            .HasIndex(i => new { i.NinjaId, i.EquipmentId, i.CategoryId })
            .IsUnique();

        modelBuilder.Entity<Ninja>().HasData(
            new Ninja { Id = 1, Name = "Ryu", Gold = 120 },
            new Ninja { Id = 2, Name = "Ken", Gold = 180 },
            new Ninja { Id = 3, Name = "Sakura", Gold = 90 },
            new Ninja { Id = 4, Name = "Hayabusa", Gold = 200 }
        );


        modelBuilder.Entity<Equipment>().HasData(
            new Equipment { Id = 1, Name = "Samurai Helmet", GoldValue = 50, CategoryId = 1, Strength = 5, Intelligence = 0, Agility = 2 },
            new Equipment { Id = 2, Name = "Hood of Shadows", GoldValue = 40, CategoryId = 1, Strength = 2, Intelligence = 0, Agility = 5 },
            new Equipment { Id = 3, Name = "Mystic Crown", GoldValue = 60, CategoryId = 1, Strength = 0, Intelligence = 8, Agility = 1 },
            new Equipment { Id = 4, Name = "Dragon Armor", GoldValue = 100, CategoryId = 2, Strength = 15, Intelligence = 0, Agility = -1 },
            new Equipment { Id = 5, Name = "Leather Vest", GoldValue = 30, CategoryId = 2, Strength = 5, Intelligence = 0, Agility = 3 },
            new Equipment { Id = 6, Name = "Mage Robe", GoldValue = 50, CategoryId = 2, Strength = 2, Intelligence = 10, Agility = 0 },
            new Equipment { Id = 7, Name = "Iron Gauntlets", GoldValue = 25, CategoryId = 3, Strength = 8, Intelligence = 0, Agility = 1 },
            new Equipment { Id = 8, Name = "Gloves of Dexterity", GoldValue = 45, CategoryId = 3, Strength = 3, Intelligence = 0, Agility = 7 },
            new Equipment { Id = 9, Name = "Sorcerer's Gloves", GoldValue = 35, CategoryId = 3, Strength = 1, Intelligence = 6, Agility = 2 },
            new Equipment { Id = 10, Name = "Steel Boots", GoldValue = 40, CategoryId = 4, Strength = 7, Intelligence = 0, Agility = -1 },
            new Equipment { Id = 11, Name = "Ninja Tabi", GoldValue = 55, CategoryId = 4, Strength = 2, Intelligence = 0, Agility = 9 },
            new Equipment { Id = 12, Name = "Boots of Insight", GoldValue = 30, CategoryId = 4, Strength = 1, Intelligence = 4, Agility = 3 },
            new Equipment { Id = 13, Name = "Ring of Power", GoldValue = 70, CategoryId = 5, Strength = 10, Intelligence = 0, Agility = 0 },
            new Equipment { Id = 14, Name = "Ring of Wisdom", GoldValue = 65, CategoryId = 5, Strength = 0, Intelligence = 10, Agility = 1 },
            new Equipment { Id = 15, Name = "Agility Band", GoldValue = 45, CategoryId = 5, Strength = 1, Intelligence = 0, Agility = 8 },
            new Equipment { Id = 16, Name = "Necklace of Fortitude", GoldValue = 90, CategoryId = 6, Strength = 12, Intelligence = 0, Agility = -2 },
            new Equipment { Id = 17, Name = "Pendant of Wisdom", GoldValue = 80, CategoryId = 6, Strength = 0, Intelligence = 12, Agility = 2 },
            new Equipment { Id = 18, Name = "Charm of Agility", GoldValue = 70, CategoryId = 6, Strength = 1, Intelligence = 0, Agility = 10 }
        );


        modelBuilder.Entity<Inventory>().HasData(
            new Inventory { NinjaId = 1, EquipmentId = 1, CategoryId = 1 },
            new Inventory { NinjaId = 1, EquipmentId = 4, CategoryId = 2 },
            new Inventory { NinjaId = 2, EquipmentId = 8, CategoryId = 3 },
            new Inventory { NinjaId = 3, EquipmentId = 12, CategoryId = 4 },
            new Inventory { NinjaId = 4, EquipmentId = 15, CategoryId = 5 }
        );


        modelBuilder.Entity<Shop>().HasData(
            new Shop { Id = 1, NinjaId = 1, EquipmentId = 1, IsAvailable = false },
            new Shop { Id = 2, NinjaId = 1, EquipmentId = 4, IsAvailable = false },
            new Shop { Id = 3, NinjaId = 2, EquipmentId = 8, IsAvailable = false },
            new Shop { Id = 4, NinjaId = 3, EquipmentId = 12, IsAvailable = false },
            new Shop { Id = 5, NinjaId = 4, EquipmentId = 15, IsAvailable = false },
            new Shop { Id = 6, NinjaId = 1, EquipmentId = 2, IsAvailable = true },
            new Shop { Id = 7, NinjaId = 2, EquipmentId = 5, IsAvailable = true },
            new Shop { Id = 8, NinjaId = 3, EquipmentId = 9, IsAvailable = true },
            new Shop { Id = 9, NinjaId = 4, EquipmentId = 14, IsAvailable = true }
        );

        modelBuilder.Entity<Category>().HasData(
             new Category { Id = 1, Name = "Head" },
             new Category { Id = 2, Name = "Chest" },
             new Category { Id = 3, Name = "Hand" },
             new Category { Id = 4, Name = "Feet" },
             new Category { Id = 5, Name = "Ring" },
             new Category { Id = 6, Name = "Necklace" }
        );

        base.OnModelCreating(modelBuilder);
    }
}
