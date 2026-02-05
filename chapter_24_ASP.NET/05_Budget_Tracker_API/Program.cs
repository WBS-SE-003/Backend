using BudgetApi.Api.Endpoints;
using BudgetApi.Infrastructure;
using BudgetApi.Services.Interfaces;
using BudgetApi.Services.EF;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddOpenApi();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
}); 

var app = builder.Build();

Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine("📘 OpenAPI docs available at:");
Console.WriteLine("   → http://localhost:3000/openapi/v1.json");
Console.WriteLine("📗 Scalar UI available at:");
Console.WriteLine("   → http://localhost:3000/scalar/v1");
Console.ResetColor();

// ENDPOINTS
app.MapOpenApi();
app.MapScalarApiReference();
app.MapTransactionEndpoints();
app.MapReportEndpoints();


app.Run();
