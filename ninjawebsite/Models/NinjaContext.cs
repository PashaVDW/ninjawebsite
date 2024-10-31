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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Inventory>()
            .HasKey(i => new { i.NinjaId, i.EquipmentId });

        modelBuilder.Entity<Inventory>()
            .HasOne(i => i.Ninja)
            .WithMany(n => n.Inventory)
            .HasForeignKey(i => i.NinjaId);

        modelBuilder.Entity<Inventory>()
            .HasOne(i => i.Equipment)
            .WithMany(e => e.Inventory)
            .HasForeignKey(i => i.EquipmentId);

        modelBuilder.Entity<Ninja>().HasData(
            new Ninja { Id = 1, Name = "Ryu", Gold = 100 },
            new Ninja { Id = 2, Name = "Ken", Gold = 150 }
        );

        modelBuilder.Entity<Equipment>().HasData(
            new Equipment { Id = 1, Name = "Katana", GoldValue = 50, Category = Category.Hand, Strength = 10, Intelligence = 0, Agility = 5 },
            new Equipment { Id = 2, Name = "Helmet", GoldValue = 30, Category = Category.Head, Strength = 5, Intelligence = 0, Agility = 2 },
            new Equipment { Id = 3, Name = "Armor", GoldValue = 75, Category = Category.Chest, Strength = 20, Intelligence = 0, Agility = -2 }
        );

        modelBuilder.Entity<Inventory>().HasData(
            new Inventory { NinjaId = 1, EquipmentId = 1, Quantity = 1 },
            new Inventory { NinjaId = 2, EquipmentId = 2, Quantity = 1 },
            new Inventory { NinjaId = 1, EquipmentId = 3, Quantity = 1 }
        );

        modelBuilder.Entity<Shop>().HasData(
            new Shop { Id = 1, NinjaId = 1, EquipmentId = 2, IsAvailable = true },
            new Shop { Id = 2, NinjaId = 2, EquipmentId = 3, IsAvailable = false }
        );

        base.OnModelCreating(modelBuilder);
    }
}
