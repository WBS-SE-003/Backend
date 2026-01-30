var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/entries", (JournalEntryRequestDto entry) =>
{
    var journalEntry = new JournalEntry
    {
        Id = Guid.NewGuid(),
        Title = entry.Title,
        Content = entry.Content,
        CreatedAt = DateTime.UtcNow
    };

    return Results.Created($"/entries/{journalEntry.Id}", journalEntry);
}).WithValidation<JournalEntryRequestDto>();

// .AddEndpointFilter<ValidationFilter<JournalEntryRequestDto>>();



app.Run();
