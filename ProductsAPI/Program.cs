using Microsoft.EntityFrameworkCore;
using ProductsAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adds support for controllers in the application.
builder.Services.AddControllers();

// Adds support for database operations.
builder.Services.AddDbContext<ProductsContext>(x => x.UseSqlite("Data Source=products.db"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enforces HTTPS for all requests.
app.UseHttpsRedirection();

// Map controller routes to enable the application to handle HTTP requests.
app.MapControllers();

app.Run();
