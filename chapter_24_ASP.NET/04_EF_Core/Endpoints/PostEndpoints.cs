using BlogApi.Dtos.Posts;
using BlogApi.Filters;
using BlogApi.Services.Interfaces;

namespace BlogApi.Endpoints;

public static class PostEndpoints
{
    public static RouteGroupBuilder MapPosts(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/posts").WithTags("Posts");

        // GET /posts
        group.MapGet("/", async (IPostService posts) =>
        {
            var list = await posts.ListAsync();

            var response = list.Select(p =>
                new PostResponseDto(p.Id, p.UserId, p.Title, p.Content));

            return Results.Ok(response);
        });

        // GET /posts/{id}
        group.MapGet("/{id:guid}", async (Guid id, IPostService posts) =>
        {
            var post = await posts.GetAsync(id);

            if (post is null)
                return Results.NotFound();

            return Results.Ok(new PostResponseDto(
                post.Id, post.UserId, post.Title, post.Content
            ));
        });

        // POST /posts
        group.MapPost("/", async (CreatePostDto dto, IUserService users, IPostService posts) =>
        {
            var user = await users.GetAsync(dto.UserId);

            if (user is null)
                return Results.BadRequest(new { error = "UserId does not exist." });

            var created = await posts.CreateAsync(dto.UserId, dto.Title, dto.Content);

            var response = new PostResponseDto(
                created.Id, created.UserId, created.Title, created.Content
            );

            return Results.Created($"/posts/{created.Id}", response);
        })
        .WithValidation<CreatePostDto>();

        // PATCH /posts/{id}
        group.MapPatch("/{id:guid}", async (Guid id, UpdatePostDto dto, IPostService posts) =>
        {
            var updated = await posts.UpdateAsync(id, dto.Title, dto.Content);

            if (updated is null)
                return Results.NotFound();

            return Results.Ok(new PostResponseDto(
                updated.Id, updated.UserId, updated.Title, updated.Content
            ));
        })
        .WithValidation<UpdatePostDto>();

        // DELETE /posts/{id}
        group.MapDelete("/{id:guid}", async (Guid id, IPostService posts) =>
        {
            var deleted = await posts.DeleteAsync(id);

            if (!deleted)
                return Results.NotFound();

            return Results.NoContent();
        });

        return group;
    }
}