using Microsoft.EntityFrameworkCore;
using ninjawebsite.Interfaces;
using ninjawebsite.Models;
using ninjawebsite.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<NinjaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NinjaDatabase")));

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<INinjaRepository, NinjaRepository>();
builder.Services.AddScoped<IShopRepository, ShopRepository>();
builder.Services.AddScoped<IEquipmentRepository, EquipmentRepository>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();

var app = builder.Build();

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
    pattern: "{controller=Ninja}/{action=Index}/{id?}");

app.Run();
