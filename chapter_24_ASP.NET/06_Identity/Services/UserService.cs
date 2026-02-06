using BlogApi.Infrastructure;
using BlogApi.Models;
using BlogApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Services;

public sealed class UserService : IUserService
{
    private readonly ApplicationDbContext _db;
    public UserService(ApplicationDbContext db) => _db = db;

    public async Task<User?> GetAsync(Guid id)
        => await _db.Users.FindAsync(id);

    public async Task<IReadOnlyList<User>> ListAsync()
        => await _db.Users.ToListAsync();

    public async Task<User> CreateAsync(string name, string email)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = name,
            Email = email,
            CreatedAt = DateTimeOffset.UtcNow
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }

    public async Task<User?> UpdateAsync(Guid id, string? name, string? email)
    {
        var user = await _db.Users.FindAsync(id);
        if (user is null) return null;

        if (!string.IsNullOrWhiteSpace(name))
            user.Name = name;

        if (!string.IsNullOrWhiteSpace(email))
            user.Email = email;

        await _db.SaveChangesAsync();
        return user;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var user = await _db.Users.FindAsync(id);
        if (user is null) return false;

        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
        return true;
    }
}