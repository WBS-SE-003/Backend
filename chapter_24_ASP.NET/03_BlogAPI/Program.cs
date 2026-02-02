using BlogApi.Endpoints;
using BlogApi.Services.Interfaces;
using BLogApi.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IUserService, InMemoryUserService>();
builder.Services.AddSingleton<IPostService, InMemoryPostService>();
builder.Services.AddOpenApi();

var app = builder.Build();

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