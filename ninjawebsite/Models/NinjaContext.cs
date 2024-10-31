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
            new Ninja { Id = 1, Name = "Ryu", Gold = 100 },
            new Ninja { Id = 2, Name = "Ken", Gold = 150 }
        );

        modelBuilder.Entity<Equipment>().HasData(
            new Equipment { Id = 1, Name = "Katana", GoldValue = 50, CategoryId = 1, Strength = 10, Intelligence = 0, Agility = 5 },
            new Equipment { Id = 2, Name = "Helmet", GoldValue = 30, CategoryId = 2, Strength = 5, Intelligence = 0, Agility = 2 },
            new Equipment { Id = 3, Name = "Armor", GoldValue = 75, CategoryId = 6, Strength = 20, Intelligence = 0, Agility = -2 }
        );

        modelBuilder.Entity<Inventory>().HasData(
            new Inventory { NinjaId = 1, EquipmentId = 1, CategoryId = 5 },
            new Inventory { NinjaId = 2, EquipmentId = 2, CategoryId = 2 },
            new Inventory { NinjaId = 1, EquipmentId = 3, CategoryId = 3 }
        );

        modelBuilder.Entity<Shop>().HasData(
            new Shop { Id = 1, NinjaId = 1, EquipmentId = 2, IsAvailable = true },
            new Shop { Id = 2, NinjaId = 2, EquipmentId = 3, IsAvailable = false }
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
