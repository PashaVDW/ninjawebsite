using Microsoft.EntityFrameworkCore;
using ninjawebsite.Interfaces;
using ninjawebsite.Models;
using ninjawebsite.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Voeg de DbContext toe met de connection string
builder.Services.AddDbContext<NinjaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NinjaDatabase")));

// Add services to the container.
builder.Services.AddControllersWithViews();

// Voeg de repository toe aan de dependency-injectiecontainer
builder.Services.AddScoped<INinjaRepository, NinjaRepository>();
builder.Services.AddScoped<IShopRepository, ShopRepository>();
builder.Services.AddScoped<IEquipmentRepository, EquipmentRepository>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();

// Build de applicatie
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
