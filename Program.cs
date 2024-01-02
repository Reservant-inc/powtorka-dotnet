var builder = WebApplication.CreateBuilder(args);

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
}

app.MapControllers();

app.Run();
