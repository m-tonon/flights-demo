using Microsoft.OpenApi.Models;
using Flights.Data;
using Flights.Domain.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add db context
builder.Services.AddDbContext<Entities>(options =>
  options.UseSqlServer(
    "Data Source=localhost,1433;"+
    "Database=Flights;"+
    "User id=developer;"+
    "Password=Pwd12345;"+
    "Encrypt=False;" +
    "TrustServerCertificate=True;"
  )
); // otherwise it will be disposed after the first request

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen(c =>
{
  c.AddServer(new OpenApiServer
  {
    Description = "Development Server",
    Url = "https://localhost:7295"
  });

  c.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["action"]} + {e.ActionDescriptor.RouteValues["controller"]}");
});

builder.Services.AddScoped<Entities>();

var app = builder.Build();

// this will be used to seed the database
var entities = app.Services.CreateScope().ServiceProvider.GetService<Entities>();

entities.Database.EnsureCreated(); // create the database if it doesn't exist

var random = new Random();

if(!entities.Flights.Any())
{
  Flight[] flightsToSeed = new Flight[]
{
  new ( Guid.NewGuid(),
              "American Airlines",
              random.Next(90, 5000).ToString(),
              new TimePlace("Los Angeles", DateTime.Now.AddHours(random.Next(1,3))),
              new TimePlace("Istambul", DateTime.Now.AddHours(random.Next(4,10))),
              2),
        new ( Guid.NewGuid(),
              "Deutsche BA",
              random.Next(90, 5000).ToString(),
              new TimePlace("Munchen", DateTime.Now.AddHours(random.Next(1,10))),
              new TimePlace("Schipol", DateTime.Now.AddHours(random.Next(4,15))),
              random.Next(1, 853)),
        new ( Guid.NewGuid(),
              "British Airways",
              random.Next(90, 5000).ToString(),
              new TimePlace("London England", DateTime.Now.AddHours(random.Next(1,15))),
              new TimePlace("Vizzola-Ticino", DateTime.Now.AddHours(random.Next(4,18))),
              random.Next(1, 853)),
        new ( Guid.NewGuid(),
              "Air France",
              random.Next(90, 5000).ToString(),
              new TimePlace("Paris", DateTime.Now.AddHours(random.Next(1,20))),
              new TimePlace("Munchen", DateTime.Now.AddHours(random.Next(4,25))),
              random.Next(1, 853)),
        new ( Guid.NewGuid(),
              "KLM",
              random.Next(90, 5000).ToString(),
              new TimePlace("Schipol", DateTime.Now.AddHours(random.Next(1,30))),
              new TimePlace("London England", DateTime.Now.AddHours(random.Next(4,35))),
              random.Next(1, 853)),
};
  entities.Flights.AddRange(flightsToSeed);

  entities.SaveChanges();
}


app.UseCors(builder => builder
  .WithOrigins("*")
  .AllowAnyMethod()
  .AllowAnyHeader()
);

app.UseSwagger().UseSwaggerUI();

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

app.Run();
