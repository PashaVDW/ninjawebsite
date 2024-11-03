﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataLayer.Migrations
{
    [DbContext(typeof(NinjaContext))]
    partial class NinjaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ninjawebsite.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Head"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Chest"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Hand"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Feet"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Ring"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Necklace"
                        });
                });

            modelBuilder.Entity("ninjawebsite.Models.Equipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Agility")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<decimal>("GoldValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Intelligence")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Strength")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Equipments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Agility = 2,
                            CategoryId = 1,
                            GoldValue = 50m,
                            Intelligence = 0,
                            Name = "Samurai Helmet",
                            Strength = 5
                        },
                        new
                        {
                            Id = 2,
                            Agility = 5,
                            CategoryId = 1,
                            GoldValue = 40m,
                            Intelligence = 0,
                            Name = "Hood of Shadows",
                            Strength = 2
                        },
                        new
                        {
                            Id = 3,
                            Agility = 1,
                            CategoryId = 1,
                            GoldValue = 60m,
                            Intelligence = 8,
                            Name = "Mystic Crown",
                            Strength = 0
                        },
                        new
                        {
                            Id = 4,
                            Agility = -1,
                            CategoryId = 2,
                            GoldValue = 100m,
                            Intelligence = 0,
                            Name = "Dragon Armor",
                            Strength = 15
                        },
                        new
                        {
                            Id = 5,
                            Agility = 3,
                            CategoryId = 2,
                            GoldValue = 30m,
                            Intelligence = 0,
                            Name = "Leather Vest",
                            Strength = 5
                        },
                        new
                        {
                            Id = 6,
                            Agility = 0,
                            CategoryId = 2,
                            GoldValue = 50m,
                            Intelligence = 10,
                            Name = "Mage Robe",
                            Strength = 2
                        },
                        new
                        {
                            Id = 7,
                            Agility = 1,
                            CategoryId = 3,
                            GoldValue = 25m,
                            Intelligence = 0,
                            Name = "Iron Gauntlets",
                            Strength = 8
                        },
                        new
                        {
                            Id = 8,
                            Agility = 7,
                            CategoryId = 3,
                            GoldValue = 45m,
                            Intelligence = 0,
                            Name = "Gloves of Dexterity",
                            Strength = 3
                        },
                        new
                        {
                            Id = 9,
                            Agility = 2,
                            CategoryId = 3,
                            GoldValue = 35m,
                            Intelligence = 6,
                            Name = "Sorcerer's Gloves",
                            Strength = 1
                        },
                        new
                        {
                            Id = 10,
                            Agility = -1,
                            CategoryId = 4,
                            GoldValue = 40m,
                            Intelligence = 0,
                            Name = "Steel Boots",
                            Strength = 7
                        },
                        new
                        {
                            Id = 11,
                            Agility = 9,
                            CategoryId = 4,
                            GoldValue = 55m,
                            Intelligence = 0,
                            Name = "Ninja Tabi",
                            Strength = 2
                        },
                        new
                        {
                            Id = 12,
                            Agility = 3,
                            CategoryId = 4,
                            GoldValue = 30m,
                            Intelligence = 4,
                            Name = "Boots of Insight",
                            Strength = 1
                        },
                        new
                        {
                            Id = 13,
                            Agility = 0,
                            CategoryId = 5,
                            GoldValue = 70m,
                            Intelligence = 0,
                            Name = "Ring of Power",
                            Strength = 10
                        },
                        new
                        {
                            Id = 14,
                            Agility = 1,
                            CategoryId = 5,
                            GoldValue = 65m,
                            Intelligence = 10,
                            Name = "Ring of Wisdom",
                            Strength = 0
                        },
                        new
                        {
                            Id = 15,
                            Agility = 8,
                            CategoryId = 5,
                            GoldValue = 45m,
                            Intelligence = 0,
                            Name = "Agility Band",
                            Strength = 1
                        },
                        new
                        {
                            Id = 16,
                            Agility = -2,
                            CategoryId = 6,
                            GoldValue = 90m,
                            Intelligence = 0,
                            Name = "Necklace of Fortitude",
                            Strength = 12
                        },
                        new
                        {
                            Id = 17,
                            Agility = 2,
                            CategoryId = 6,
                            GoldValue = 80m,
                            Intelligence = 12,
                            Name = "Pendant of Wisdom",
                            Strength = 0
                        },
                        new
                        {
                            Id = 18,
                            Agility = 10,
                            CategoryId = 6,
                            GoldValue = 70m,
                            Intelligence = 0,
                            Name = "Charm of Agility",
                            Strength = 1
                        });
                });

            modelBuilder.Entity("ninjawebsite.Models.Inventory", b =>
                {
                    b.Property<int>("NinjaId")
                        .HasColumnType("int");

                    b.Property<int>("EquipmentId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.HasKey("NinjaId", "EquipmentId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("EquipmentId");

                    b.HasIndex("NinjaId", "EquipmentId", "CategoryId")
                        .IsUnique();

                    b.ToTable("Inventories");

                    b.HasData(
                        new
                        {
                            NinjaId = 1,
                            EquipmentId = 1,
                            CategoryId = 1
                        },
                        new
                        {
                            NinjaId = 1,
                            EquipmentId = 4,
                            CategoryId = 2
                        },
                        new
                        {
                            NinjaId = 2,
                            EquipmentId = 8,
                            CategoryId = 3
                        },
                        new
                        {
                            NinjaId = 3,
                            EquipmentId = 12,
                            CategoryId = 4
                        },
                        new
                        {
                            NinjaId = 4,
                            EquipmentId = 15,
                            CategoryId = 5
                        });
                });

            modelBuilder.Entity("ninjawebsite.Models.Ninja", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Gold")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Ninjas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Gold = 120m,
                            Name = "Ryu"
                        },
                        new
                        {
                            Id = 2,
                            Gold = 180m,
                            Name = "Ken"
                        },
                        new
                        {
                            Id = 3,
                            Gold = 90m,
                            Name = "Sakura"
                        },
                        new
                        {
                            Id = 4,
                            Gold = 200m,
                            Name = "Hayabusa"
                        });
                });

            modelBuilder.Entity("ninjawebsite.Models.Shop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EquipmentId")
                        .HasColumnType("int");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<int>("NinjaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EquipmentId");

                    b.HasIndex("NinjaId");

                    b.ToTable("Shops");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EquipmentId = 1,
                            IsAvailable = false,
                            NinjaId = 1
                        },
                        new
                        {
                            Id = 2,
                            EquipmentId = 4,
                            IsAvailable = false,
                            NinjaId = 1
                        },
                        new
                        {
                            Id = 3,
                            EquipmentId = 8,
                            IsAvailable = false,
                            NinjaId = 2
                        },
                        new
                        {
                            Id = 4,
                            EquipmentId = 12,
                            IsAvailable = false,
                            NinjaId = 3
                        },
                        new
                        {
                            Id = 5,
                            EquipmentId = 15,
                            IsAvailable = false,
                            NinjaId = 4
                        },
                        new
                        {
                            Id = 6,
                            EquipmentId = 2,
                            IsAvailable = true,
                            NinjaId = 1
                        },
                        new
                        {
                            Id = 7,
                            EquipmentId = 5,
                            IsAvailable = true,
                            NinjaId = 2
                        },
                        new
                        {
                            Id = 8,
                            EquipmentId = 9,
                            IsAvailable = true,
                            NinjaId = 3
                        },
                        new
                        {
                            Id = 9,
                            EquipmentId = 14,
                            IsAvailable = true,
                            NinjaId = 4
                        });
                });

            modelBuilder.Entity("ninjawebsite.Models.Equipment", b =>
                {
                    b.HasOne("ninjawebsite.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("ninjawebsite.Models.Inventory", b =>
                {
                    b.HasOne("ninjawebsite.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ninjawebsite.Models.Equipment", "Equipment")
                        .WithMany("Inventory")
                        .HasForeignKey("EquipmentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ninjawebsite.Models.Ninja", "Ninja")
                        .WithMany("Inventory")
                        .HasForeignKey("NinjaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Equipment");

                    b.Navigation("Ninja");
                });

            modelBuilder.Entity("ninjawebsite.Models.Shop", b =>
                {
                    b.HasOne("ninjawebsite.Models.Equipment", "Equipment")
                        .WithMany()
                        .HasForeignKey("EquipmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ninjawebsite.Models.Ninja", "Ninja")
                        .WithMany()
                        .HasForeignKey("NinjaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Equipment");

                    b.Navigation("Ninja");
                });

            modelBuilder.Entity("ninjawebsite.Models.Equipment", b =>
                {
                    b.Navigation("Inventory");
                });

            modelBuilder.Entity("ninjawebsite.Models.Ninja", b =>
                {
                    b.Navigation("Inventory");
                });
#pragma warning restore 612, 618
        }
    }
}
