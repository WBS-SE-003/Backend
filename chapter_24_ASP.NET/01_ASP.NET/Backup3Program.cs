// var builder = WebApplication.CreateBuilder(args);
// var app = builder.Build();

// // Users endpoints
// app.MapGet("/users", () => new[] { "John", "Jane" });
// app.MapGet("/users/{id}", (int id) => $"User {id}");
// app.MapPost("/users", (UserRequestDTO user) => $"Created user {user.Name}");

// // Posts endpoints
// app.MapGet("/posts", () => new[] { "firstPost", "secondPost" });
// app.MapGet("/posts/{id}", (int id) => $"Post {id}");
// app.MapPost("/posts", (PostRequestDTO post) => $"Created post {post.Title}");


// // Query Parameters??
// app.MapGet("/posts/search", (string term) =>
//     // db.GetPost("nameOfThePost");
//     $"You seearched for posts with: {term}");


// app.Run();


// // DTOs
// record UserRequestDTO(string Name);
// record PostRequestDTO(string Title);
