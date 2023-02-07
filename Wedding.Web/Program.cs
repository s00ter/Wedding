using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Wedding.DAL.Data;
using Wedding.DAL.Data.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<WeddingContext>(c =>
    c.UseSqlServer(
        "Server=(localdb)\\MSSQLLocalDB;Database=Wedding;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False"));

builder.Services.AddIdentity<Client, IdentityRole>(x => x.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<WeddingContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.UseAuthorization();
app.UseAuthentication();

app.Run();