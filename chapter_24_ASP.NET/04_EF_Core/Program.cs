using BlogApi.Endpoints;
using BlogApi.Infrastructure;
using BlogApi.Infrastructure.Data;
using BlogApi.Services;
using BlogApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddSingleton<IUserService, InMemoryUserService>();
// builder.Services.AddSingleton<IPostService, InMemoryPostService>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPostService, PostService>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<DbSeeder>();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();
    await seeder.SeedAsync();
}

app.MapOpenApi();            // /openapi/v1.json
app.MapScalarApiReference(); // /scalar/v1

Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine("📘 OpenAPI docs available at:");
Console.WriteLine("   → http://localhost:3000/openapi/v1.json");
Console.WriteLine("📗 Scalar UI available at:");
Console.WriteLine("   → http://localhost:3000/scalar/v1");
Console.ResetColor();

// ENDPOINTS
app.MapUsers();
app.MapPosts();

app.Run();