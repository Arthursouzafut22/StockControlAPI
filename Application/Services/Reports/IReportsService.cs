using ControleMercadoria.Core.DTOs.Reports;

namespace ControleMercadoria.Application.Services.Reports
{
    public interface IReportsService
    {
        Task<SummaryReportsResponseDTO> GetSummaryReport(long userId);
        Task<InventoryReportsResponseDTO> GetInventoryReports(long userId);
        Task<byte[]> GeneratePdf(long userId);
    }
}
