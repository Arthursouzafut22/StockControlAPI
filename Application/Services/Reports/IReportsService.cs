using ControleMercadoria.Core.DTOs.Movements;
using ControleMercadoria.Core.DTOs.Reports;

namespace ControleMercadoria.Application.Services.Reports
{
    public interface IReportsService
    {
        Task<SummaryReportsResponseDTO> GetSummaryReport(long userId);
        Task<InventoryReportsResponseDTO> GetInventoryReports(long userId);
        Task<IEnumerable<MovementsResponseDTO>> GetMovementsByFilter(ReportPeriodFilterDTO dtoFilter, long userId);
        Task<SummaryReportsResponseDTO> GetSummaryReport(ReportPeriodFilterDTO dtoFilter, SummaryReportsResponseDTO summary, long userId);
        Task<byte[]> GeneratePdf(long userId, ReportPeriodFilterDTO dtoFilter, SummaryReportsResponseDTO summary);
        Task<byte[]> GeneratePdfByFilter(ReportPeriodFilterDTO dtoFilter, long userId);
    }
}
