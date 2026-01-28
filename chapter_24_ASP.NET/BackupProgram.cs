// var builder = WebApplication.CreateBuilder(args);

// // read config values
// var appName = builder.Configuration["AppName"] ?? "Default App";
// var greeting = builder.Configuration["Greeting"] ?? "Hi";

// var app = builder.Build();

// app.UseStaticFiles(); // to serve static files
// app.UseRouting();

// app.Use(async (context, next) =>
// {
//     Console.WriteLine($"Handling request: {context.Request.Path}");
//     await next();
//     Console.WriteLine("Finsihed handling request");
// });

// app.Use(async (context, next) =>
// {
//     if (context.Request.Path == "/forbidden")
//     {
//         context.Response.StatusCode = 403;
//         await context.Response.WriteAsync("Forbidden");
//     }
//     else
//     {
//         await next();
//     }
// });





// app.MapGet("/", () => "Hello World!");
// app.MapGet("/config", () => $"{appName} says: {greeting}");


// app.Run();
