using BudgetApi.Dtos.Reports;

namespace BudgetApi.Services.Interfaces;

public interface IReportService
{
    Task<SummaryReportResponseDto> GetSummaryAsync(DateOnly start, DateOnly end);
}