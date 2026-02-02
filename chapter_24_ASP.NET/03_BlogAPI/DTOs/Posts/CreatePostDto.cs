using System.ComponentModel.DataAnnotations;

namespace BlogApi.Dtos.Posts;

public record CreatePostDto(
    [property: Required]
    Guid UserId,

    [property: Required]
    [property: StringLength(200, MinimumLength =1)]
    string Title,

  [property: Required]
    [property: StringLength(200, MinimumLength =1)]
    string Content

);