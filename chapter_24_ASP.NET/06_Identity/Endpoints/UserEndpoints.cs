// using BlogApi.Dtos.Posts;
// using BlogApi.Dtos.Users;
// using BlogApi.Filters;
// using BlogApi.Models;
// using BlogApi.Services;
// using BlogApi.Services.Interfaces;
// using BLogApi.Services;

// namespace BlogApi.Endpoints;

// public static class UserEndpoints
// {
//     public static RouteGroupBuilder MapUsers(this IEndpointRouteBuilder routes)
//     {
//         var group = routes.MapGroup("/users").WithTags("Users");

//         // GET /users
//         group.MapGet("/", async (IUserService users) =>
//         {
//             var list = await users.ListAsync();

//             if (list.Count == 0)
//                 return Results.NotFound("No usersfound");

//             var response = list.Select(u =>
//                 new UserResponseDto(u.Id, u.Name, u.Email, u.CreatedAt));

//             return Results.Ok(response);
//         });

//         // GET /users{id}
//         group.MapGet("/{id:guid}", async (Guid id, IUserService users) =>
//         {
//             var user = await users.GetAsync(id);

//             if (user is null)
//                 return Results.NotFound();

//             return Results.Ok(new UserResponseDto(
//                 user.Id, user.Name, user.Email, user.CreatedAt
//             ));
//         });

//         // POST /users
//         group.MapPost("/", async (CreateUserDto dto, IUserService users) =>
//         {
//             var created = await users.CreateAsync(dto.Name, dto.Email);
//             var response = new UserResponseDto(
//                 created.Id, created.Name, created.Email, created.CreatedAt
//             );

//             return Results.Created($"/users/{created.Id}", response);
//         }).WithValidation<CreateUserDto>();

//         // PATCH /users{id}
//         group.MapPatch("/{id:guid}", async (Guid id, UpdateUserDto dto, IUserService users) =>
//         {
//             var updated = await users.UpdateAsync(id, dto.Name, dto.Email);

//             if (updated is null)
//                 return Results.NotFound();

//             return Results.Ok(new UserResponseDto(
//                 updated.Id, updated.Name, updated.Email, updated.CreatedAt
//             ));
//         }).WithValidation<UpdateUserDto>();

//         group.MapDelete("/{id:guid}", async (Guid id, IUserService users, IPostService posts) =>
//         {
//             var deleted = await users.DeleteAsync(id);

//             if (!deleted)
//                 return Results.NotFound();

//             if (posts is InMemoryPostService inMemoryPost)
//                 await inMemoryPost.DeleteByUserAsync(id);
//             return Results.NoContent();
//         });

//         // GET /users{id}/posts 
//         group.MapGet("/{id:guid}/posts", async (Guid id, IUserService users, IPostService posts) =>
//         {
//             var user = await users.GetAsync(id);
//             if (user is null)
//                 return Results.NotFound();

//             var list = await posts.ListByUserAsync(id);

//             var response = list.Select(p =>
//             new PostResponseDto(p.Id, p.UserId, p.Title, p.Content));

//             return Results.Ok(response);

//         });

//         return group;
//     }
// }