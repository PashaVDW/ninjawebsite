﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ninjawebsite.Migrations
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
                            Agility = 5,
                            CategoryId = 1,
                            GoldValue = 50m,
                            Intelligence = 0,
                            Name = "Katana",
                            Strength = 10
                        },
                        new
                        {
                            Id = 2,
                            Agility = 2,
                            CategoryId = 2,
                            GoldValue = 30m,
                            Intelligence = 0,
                            Name = "Helmet",
                            Strength = 5
                        },
                        new
                        {
                            Id = 3,
                            Agility = -2,
                            CategoryId = 6,
                            GoldValue = 75m,
                            Intelligence = 0,
                            Name = "Armor",
                            Strength = 20
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
                            CategoryId = 5
                        },
                        new
                        {
                            NinjaId = 2,
                            EquipmentId = 2,
                            CategoryId = 2
                        },
                        new
                        {
                            NinjaId = 1,
                            EquipmentId = 3,
                            CategoryId = 3
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
                            Gold = 100m,
                            Name = "Ryu"
                        },
                        new
                        {
                            Id = 2,
                            Gold = 150m,
                            Name = "Ken"
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
                            EquipmentId = 2,
                            IsAvailable = true,
                            NinjaId = 1
                        },
                        new
                        {
                            Id = 2,
                            EquipmentId = 3,
                            IsAvailable = false,
                            NinjaId = 2
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
