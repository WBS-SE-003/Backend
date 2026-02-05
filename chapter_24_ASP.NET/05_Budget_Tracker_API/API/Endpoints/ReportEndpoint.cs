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
            IReportService svc) => await svc.GetSummaryAsync(start, end));
    }
}