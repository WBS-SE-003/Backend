var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<TimeService>();

var app = builder.Build();


app.MapGet("/time", (TimeService ts) => ts.Now());

app.MapUsers();

app.Run();




