public static class UserEndpoints
{
    public static RouteGroupBuilder MapUsers(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/users");

        group.MapGet("/", () => new[] { "John", "Jane" });
        group.MapPost("/", () => Results.Ok);

        return group;
    }
}