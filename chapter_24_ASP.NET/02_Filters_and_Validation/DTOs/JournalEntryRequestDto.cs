using System.ComponentModel.DataAnnotations;

public record JournalEntryRequestDto(
    [property: Required]
    [property: StringLength(100, MinimumLength =1)]
    string Title,

    [property: Required]
    [property: StringLength(10000, MinimumLength =1)]
    string Content

);