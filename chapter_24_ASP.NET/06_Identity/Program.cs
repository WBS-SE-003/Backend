using System.Text;
using BlogApi.Endpoints;
using BlogApi.Infrastructure;
using BlogApi.Models;
using BlogApi.Services;
using BlogApi.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// EF Core + SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


// Identity
builder.Services.AddIdentityCore<User>()
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// JWT Auth
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

// App Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPostService, PostService>();
// builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddOpenApi();

var app = builder.Build();

// DOCs
app.MapOpenApi();
app.MapScalarApiReference();

// Auth middleware (order matters!)
app.UseAuthentication();
app.UseAuthorization();

// ENDPOINTS
app.MapAuthEndpionts();
app.MapPosts();
// app.MapUsers();

Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine("📘 OpenAPI docs available at:");
Console.WriteLine("   → http://localhost:3000/openapi/v1.json");
Console.WriteLine("📗 Scalar UI available at:");
Console.WriteLine("   → http://localhost:3000/scalar/v1");
Console.ResetColor();

app.Run();