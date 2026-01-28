// var builder = WebApplication.CreateBuilder(args);
// var app = builder.Build();


// app.MapGet("/", () => "Hello World!"); // => res.send('Hello World!')
// app.MapGet("/json", () => new { Name = "John", Role = "Student" });

// app.MapGet("/status", () =>
// {
//     // return Results.StatusCode(418);
//     return Results.Problem(statusCode: 418);
// });

// // POSTS
// app.MapPost("/test", () =>
// {
//     var post = new BlogPost(1, "Hello", "World!");

//     // return Results.Ok(post);
//     return Results.Created("/test/,", post);

// });

// app.MapPost("/posts", (BlogPost post) =>
// {
//     // db.save(postS);
//     return Results.Created("/posts", post);

// });



// app.Run();


// public record BlogPost(int id, string Title, string Content);

