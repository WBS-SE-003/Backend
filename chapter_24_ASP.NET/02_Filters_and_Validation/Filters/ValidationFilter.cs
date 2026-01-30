using System.ComponentModel.DataAnnotations;

public sealed class ValidationFilter<T> : IEndpointFilter where T : class
{
    public async ValueTask<object?> InvokeAsync(
     EndpointFilterInvocationContext context,
     EndpointFilterDelegate next)
    {


        var dto = context.Arguments.OfType<T>().FirstOrDefault();

        if (dto is null)
        {
            return Results.BadRequest("missing request body");
        }

        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(
            dto,
            new ValidationContext(dto),
            results,
            validateAllProperties: true
        );

        if (!isValid)
        {
            return Results.BadRequest(
                results.Select(r => r.ErrorMessage)
            );
        }
        return await next(context);
    }
}