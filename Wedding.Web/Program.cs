using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Wedding.DAL.Data;
using Wedding.DAL.Data.Entities;
using Wedding.DAL.Repository.Abstractions;
using Wedding.DAL.Repository.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
}));

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<WeddingContext>(c =>
    c.UseSqlServer(
        "Server=(localdb)\\MSSQLLocalDB;Database=Wedding;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False"));

builder.Services.AddIdentity<Client, IdentityRole>(x => x.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<WeddingContext>();

builder.Services.AddScoped<IWareCategoryRepository, WareCategoryRepository>();
builder.Services.AddScoped<ISalonRepository, SalonRepository>();
builder.Services.AddScoped<IWareRepository, WareRepository>();

var app = builder.Build();

app.UseCors("MyPolicy");

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