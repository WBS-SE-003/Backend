using System.ComponentModel.DataAnnotations;

namespace BlogApi.Filters;

// Generic endpoint filter used to validate DTOs before the endpoint runs
public sealed class ValidationFilter<T> : IEndpointFilter where T : class
{
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        // try to find the DTO of type T from the endpoint arguments
        // (minimal APIs automatically bind JSON to method parameters)
        var dto = context.Arguments.OfType<T>().FirstOrDefault();

        // If no DTO was provided in the request body
        // (e.g. empty or missing JSON)
        if (dto is null)
        {
            return Results.BadRequest(new { error = "Missing request body" });
        }

        // prepare validation using DataAnnotations
        var results = new List<ValidationResult>();
        var ctx = new ValidationContext(dto);

        // vvalidate all [Required], [StringLength], [EmailAddress], etc.
        bool isValid = Validator.TryValidateObject(dto, ctx, results, true);

        // If validation failed, convert errors into a format
        if (!isValid)
        {
            var errors = results
                .GroupBy(r => r.MemberNames.FirstOrDefault() ?? string.Empty)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(r => r.ErrorMessage ?? "Invalid").ToArray()
                );

            return Results.ValidationProblem(errors);
        }

        // Validation passed => continue to the actual endpoint
        return await next(context);
    }
}
