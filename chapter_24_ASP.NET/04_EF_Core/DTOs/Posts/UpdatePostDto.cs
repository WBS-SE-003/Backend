using System.ComponentModel.DataAnnotations;

namespace BlogApi.Dtos.Posts;

public record UpdatePostDto(
    [property: StringLength(200, MinimumLength =1)]
    string? Title,

    [property: StringLength(200, MinimumLength =1)]
    string? Content
);