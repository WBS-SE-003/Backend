using BudgetApi.Dtos.Reports;

namespace BudgetApi.Services.Interfaces;

public interface IReportService
{
    Task<SummaryReportResponseDto> GetSummaryAsync(
        Guid userId, DateOnly start, DateOnly end);
}