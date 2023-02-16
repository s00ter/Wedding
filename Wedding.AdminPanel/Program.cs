using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Wedding.DAL.Data;
using Wedding.DAL.Data.Entities;
using Wedding.DAL.Repository.Abstractions;
using Wedding.DAL.Repository.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<WeddingContext>(c =>
    c.UseSqlServer(
        "Server=(localdb)\\MSSQLLocalDB;Database=Wedding;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False"));

builder.Services.AddIdentity<Client, IdentityRole>(x => x.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<WeddingContext>();
builder.Services.AddScoped<ICityRepository,CityRepository>();
builder.Services.AddScoped<IWareRepository,WareRepository>();
builder.Services.AddScoped<IWareCategoryRepository, WareCategoryRepository>();
builder.Services.AddScoped<ISalonRepository, SalonRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
