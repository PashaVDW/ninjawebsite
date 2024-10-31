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
            new Ninja { Id = 1, Naam = "Ryu", Goud = 100 },
            new Ninja { Id = 2, Naam = "Ken", Goud = 150 }
        );

        modelBuilder.Entity<Equipment>().HasData(
            new Equipment { Id = 1, Naam = "Katana", WaardeGoud = 50, Categorie = Categorie.Hand, Strength = 10, Intelligence = 0, Agility = 5 },
            new Equipment { Id = 2, Naam = "Helm", WaardeGoud = 30, Categorie = Categorie.Head, Strength = 5, Intelligence = 0, Agility = 2 },
            new Equipment { Id = 3, Naam = "Harnas", WaardeGoud = 75, Categorie = Categorie.Chest, Strength = 20, Intelligence = 0, Agility = -2 }
        );

        modelBuilder.Entity<Inventory>().HasData(
            new Inventory { NinjaId = 1, EquipmentId = 1, Aantal = 1 },
            new Inventory { NinjaId = 2, EquipmentId = 2, Aantal = 1 },
            new Inventory { NinjaId = 1, EquipmentId = 3, Aantal = 1 }
        );

        modelBuilder.Entity<Shop>().HasData(
            new Shop { Id = 1, NinjaId = 1, EquipmentId = 2, IsBeschikbaar = true },
            new Shop { Id = 2, NinjaId = 2, EquipmentId = 3, IsBeschikbaar = false }
        );

        base.OnModelCreating(modelBuilder);
    }
}
