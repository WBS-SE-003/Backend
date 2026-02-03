using BlogApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Infrastructure.Data;

public sealed class DbSeeder
{
    private readonly ApplicationDbContext _db;
    public DbSeeder(ApplicationDbContext db) => _db = db;

    public async Task SeedAsync(CancellationToken ct = default)
    {
        await _db.Database.MigrateAsync(ct);

        if (await _db.Users.AnyAsync(ct))
            return;

        var toni = new User
        {
            Id = Guid.NewGuid(),
            Name = "Toni",
            Email = "toni@example.com",
            CreatedAt = DateTimeOffset.UtcNow
        };

        var oualid = new User
        {
            Id = Guid.NewGuid(),
            Name = "Oualid",
            Email = "oualid@example.com",
            CreatedAt = DateTimeOffset.UtcNow
        };

        _db.Users.AddRange(toni, oualid);
        await _db.SaveChangesAsync(ct);

        _db.Posts.AddRange(
           new Post
           {
               Id = Guid.NewGuid(),
               UserId = toni.Id,
               Title = "Hello World",
               Content = "First post"
           },
           new Post
           {
               Id = Guid.NewGuid(),
               UserId = oualid.Id,
               Title = "Notes",
               Content =
            "Second post",
           }
       );
        await _db.SaveChangesAsync(ct);
    }
}