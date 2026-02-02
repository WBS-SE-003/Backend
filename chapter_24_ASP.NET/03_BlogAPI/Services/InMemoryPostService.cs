using BlogApi.Models;
using BlogApi.Services.Interfaces;

namespace BLogApi.Services;

public sealed class InMemoryPostService : IPostService
{
    private readonly Dictionary<Guid, Post> _posts = new();

    public Task<Post?> GetAsync(Guid id)
    {
        _posts.TryGetValue(id, out var post);
        return Task.FromResult(post);
    }

    public Task<IReadOnlyList<Post>> ListAsync()
        => Task.FromResult((IReadOnlyList<Post>)_posts.Values.ToList());

    public Task<IReadOnlyList<Post>> ListByUserAsync(Guid userId)
    {
        var list = _posts.Values.Where(p => p.UserId == userId).ToList();
        return Task.FromResult((IReadOnlyList<Post>)list);
    }

    public Task<Post> CreateAsync(Guid userId, string title, string content)
    {
        var post = new Post
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Title = title,
            Content = content,
        };

        _posts[post.Id] = post;
        return Task.FromResult(post);
    }

    public Task<Post?> UpdateAsync(Guid id, string? title, string? content)
    {
        if (!_posts.TryGetValue(id, out var post))
            return Task.FromResult<Post?>(null);

        if (!string.IsNullOrWhiteSpace(title))
            post.Title = title;

        if (!string.IsNullOrWhiteSpace(content))
            post.Content = content;

        return Task.FromResult<Post?>(post);
    }

    public Task<bool> DeleteAsync(Guid id)
        => Task.FromResult(_posts.Remove(id));

    public Task DeleteByUserAsync(Guid userId)
    {
        var ids = _posts.Values
        .Where(p => p.UserId == userId)
        .Select(p => p.Id)
        .ToList();

        foreach (var id in ids)
            _posts.Remove(id);
        return Task.CompletedTask;
    }


}

