using Microsoft.EntityFrameworkCore;
using Powtorka.Data;

Console.WriteLine("Hello, world!");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PowtorkaDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

builder.Services.AddControllers();

// Dodajemy Swagger.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Włączamy Swagger tylko podczas debugowania.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Updatujemy bazę danych (pozwala nie używać migracji).
    using var scope = app.Services.CreateScope();
    await scope.ServiceProvider
        .GetService<PowtorkaDbContext>()!
        .Database
        .EnsureCreatedAsync();
}

app.MapControllers();

app.Run();
