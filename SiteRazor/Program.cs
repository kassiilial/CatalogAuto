using AutoMapper;
using BusinessLogic;
using Entities;
using Microsoft.EntityFrameworkCore;
using MyLogger;
using SiteRazor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddAutoMapper(typeof(AppMappingProfileForRazor));
builder.Services.AddTransient<ICarRepository, CarRepository>();
builder.Services.AddTransient<ILogger<CarRepository>, StandartLogger<CarRepository>>();
builder.Services.AddTransient<ICarServices, CarServices>();
builder.Services.AddTransient<ILogger<CarServices>, StandartLogger<CarServices>>();
builder.Services.AddDbContext<AppCarContext>(option => option.UseNpgsql("Host=localhost;Port=5432;Database=autotest;Username=postgres;Password=12345"));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();

app.Run();