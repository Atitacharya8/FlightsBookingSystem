using FlightsBookingSystem.Data;
using FlightsBookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
//Add db context
builder.Services.AddDbContext<Entities>(options => options.UseInMemoryDatabase(databaseName: "Flights"), ServiceLifetime.Singleton);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen(c =>
{
    c.AddServer(new OpenApiServer
    {
        Description = "Development Server",
        Url = "https://localhost:7243"
    });

    c.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["action"] + e.ActionDescriptor.RouteValues["controller"]}");
});

builder.Services.AddSingleton<Entities>();

var app = builder.Build();

//seeding data
var entities = app.Services.CreateScope().ServiceProvider.GetService<Entities>();

var Random = new Random();

Flight[] flightsToSeed = new Flight[]
{
    new (Guid.NewGuid(),
                    "American Airlines",
                    Random.Next(90, 5000). ToString(),
                    new TimePlace("Los Angeles", DateTime.Now.AddHours(Random.Next(1,3))),
                    new TimePlace("Istanbul", DateTime.Now.AddHours(Random.Next(4,10))),
                    2),

           new (Guid.NewGuid(),
                "Deutsche BA",
                Random.Next(90, 5000). ToString(),
                new TimePlace("Munchen", DateTime.Now.AddHours(Random.Next(1,3))),
                new TimePlace("Schipol", DateTime.Now.AddHours(Random.Next(4,10))),
                Random.Next(1,853)),

           new (Guid.NewGuid(),
                "American Airlines",
                Random.Next(90, 5000). ToString(),
                new TimePlace("London, England", DateTime.Now.AddHours(Random.Next(1,3))),
                new TimePlace("Vizzola-ticino", DateTime.Now.AddHours(Random.Next(4,10))),
                Random.Next(1,853)),

           new (Guid.NewGuid(),
                "Basiq Air",
                Random.Next(90, 5000). ToString(),
                new TimePlace("Amsterdam", DateTime.Now.AddHours(Random.Next(1,3))),
                new TimePlace("Glasgow, Scotland", DateTime.Now.AddHours(Random.Next(4,10))),
                Random.Next(1,853)),

           new (Guid.NewGuid(),
                "BB Heliag",
                Random.Next(90, 5000). ToString(),
                new TimePlace("Zurich", DateTime.Now.AddHours(Random.Next(1,3))),
                new TimePlace("Baku", DateTime.Now.AddHours(Random.Next(4,10))),
                Random.Next(1,853)),

           new (Guid.NewGuid(),
                "ABA Air",
                Random.Next(90, 5000). ToString(),
                new TimePlace("Praha Ruzyne", DateTime.Now.AddHours(Random.Next(1,3))),
                new TimePlace("Paris", DateTime.Now.AddHours(Random.Next(4,10))),
                Random.Next(1,853)),

           new (Guid.NewGuid(),
                "AB Corporate Aviation",
                Random.Next(90, 5000). ToString(),
                new TimePlace("Le Bourget", DateTime.Now.AddHours(Random.Next(1,3))),
                new TimePlace("Zagreb", DateTime.Now.AddHours(Random.Next(4,10))),
                Random.Next(1,853))
};

entities.Flights.AddRange(flightsToSeed);

entities.SaveChanges();

app.UseCors(builder => builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader());

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
    pattern : "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
