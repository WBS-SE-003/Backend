using System.Security.Claims;
using BudgetApi.Services.Interfaces;

namespace BudgetApi.Api.Endpoints;

public static class ReportEndpoints
{
    public static void MapReportEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/reports").WithTags("Reports");

        group.MapGet("/summary", async (
            DateOnly start,
            DateOnly end,
            ClaimsPrincipal user,
            IReportService svc) =>
        {
            var userId = GetUserId(user);
            if (userId is null) return Results.Unauthorized();

            var summary = await svc.GetSummaryAsync(userId.Value, start, end);
            return Results.Ok(summary);
        });
    }

    private static Guid? GetUserId(ClaimsPrincipal user)
    {
        var raw = user.FindFirstValue(ClaimTypes.NameIdentifier);
        return Guid.TryParse(raw, out var id) ? id : null;
    }
}