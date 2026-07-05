using ControleMercadoria.Application.Services.Movements;
using ControleMercadoria.Application.Services.Products;
using ControleMercadoria.Application.Services.Reports.Documents;
using ControleMercadoria.Core.DTOs.Movements;
using ControleMercadoria.Core.DTOs.Reports;
using QuestPDF.Fluent;
using System.Globalization;

namespace ControleMercadoria.Application.Services.Reports
{
    public class ReportsService : IReportsService
    {
        private readonly IProductService _serviceProduct;
        private readonly IMovementService _serviceMovement;
        private readonly ILogger<MovementService> _logger;

        public ReportsService(IProductService serviceProduct,
            IMovementService serviceMovement, ILogger<MovementService> logger)
        {
            _serviceProduct = serviceProduct;
            _serviceMovement = serviceMovement;
            _logger = logger;

        }

        public async Task<SummaryReportsResponseDTO> GetSummaryReport(long userId)
        {
            var entryMovements = await _serviceMovement.GetEntryMovements(userId);
            var exitMovements = await _serviceMovement.GetExitMovements(userId);

            var totalSpent = entryMovements.Sum(x => x.TotalValue);
            var totalSold = exitMovements.Sum(x => x.TotalValue);

            var profit = totalSold - totalSpent;
            var isProfitable = totalSold > totalSpent;

            return new SummaryReportsResponseDTO(
                totalSpent,
                totalSold,
                profit,
                isProfitable
            );
        }

        public async Task<InventoryReportsResponseDTO> GetInventoryReports(long userId)
        {
            var products = await _serviceProduct.GetAll(userId);
            var totalStockValue = products.Select(x => x.PriceCost * x.StockQuantity);

            return new InventoryReportsResponseDTO(totalStockValue.Sum());
        }

        public async Task<IEnumerable<MovementsResponseDTO>> GetMovementsByFilter(
            ReportPeriodFilterDTO dtoFilter, long userId)
        {
            var movementsAll = await _serviceMovement.GetAll(userId);

            DateTime? inicio = null;
            DateTime? fim = null;

            if (!string.IsNullOrWhiteSpace(dtoFilter?.dataInicio))
            {
                inicio = DateTime.ParseExact(
                    dtoFilter.dataInicio,
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture);
            }

            if (!string.IsNullOrWhiteSpace(dtoFilter?.dataFim))
            {
                fim = DateTime.ParseExact(
                    dtoFilter.dataFim,
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture)
                    .AddDays(1);
            }

            var movementsBetweenDates = movementsAll.Where(x =>
                x.CreatedAt >= inicio &&
                x.CreatedAt < fim);

            var movementsForASingleDate = movementsAll.Where(x =>
                x.CreatedAt >= inicio ||
                x.CreatedAt < fim);

            var movements = movementsBetweenDates.Count() == 0
                ? movementsForASingleDate
                : movementsBetweenDates;

            var result = dtoFilter?.dataInicio == null &&
                         dtoFilter?.dataFim == null
                ? movementsAll
                : movements;

            return result;
        }

        public async Task<SummaryReportsResponseDTO> GetSummaryReport(
            ReportPeriodFilterDTO dtoFilter, SummaryReportsResponseDTO summary, long userId)
        {
            var summaryAll = await GetSummaryReport(userId);
            var summaryReport = (dtoFilter.dataInicio == null && dtoFilter.dataFim == null) ? summaryAll : summary;
            return summaryReport;
        }

        public async Task<byte[]> GeneratePdf(long userId,
           ReportPeriodFilterDTO dtoFilter,
           SummaryReportsResponseDTO summary)
        {
            var movements = await _serviceMovement.GetAll(userId);
            var products = await _serviceProduct.GetAll(userId);
            var movementsByFilter = await GetMovementsByFilter(dtoFilter, userId);

            var document = new MovementReportDocument(
                movementsByFilter,
                products,
                summary,
                dtoFilter);

            return document.GeneratePdf();
        }

        public async Task<byte[]> GeneratePdfByFilter( ReportPeriodFilterDTO? dtoFilter,
            long userId)
        {
            if (!string.IsNullOrEmpty(dtoFilter?.dataInicio))
            {
                var valido = DateTime.TryParseExact(
                    dtoFilter.dataInicio,
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out _);

                if (!valido)
                {
                    throw new InvalidOperationException(
                        "dataInicio inválida. Use o formato dd/MM/yyyy.");
                }
            }

            if (!string.IsNullOrEmpty(dtoFilter?.dataFim))
            {
                var valido = DateTime.TryParseExact(
                    dtoFilter.dataFim,
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out _);

                if (!valido)
                {
                    throw new InvalidOperationException(
                        "dataFim inválida. Use o formato dd/MM/yyyy.");
                }
            }

            DateTime? inicio = null;
            DateTime? fim = null;

            if (!string.IsNullOrWhiteSpace(dtoFilter?.dataInicio))
            {
                inicio = DateTime.ParseExact(
                    dtoFilter.dataInicio,
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture);
            }

            if (!string.IsNullOrWhiteSpace(dtoFilter?.dataFim))
            {
                fim = DateTime.ParseExact(
                    dtoFilter.dataFim,
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture)
                    .AddDays(1);
            }

            var entryMovements = await _serviceMovement.GetEntryMovements(userId);
            var exitMovements = await _serviceMovement.GetExitMovements(userId);
            var products = await _serviceProduct.GetAll(userId);

            var filterTotalSpentBetweenDates = entryMovements.Where(x =>
                x.CreatedAt >= inicio &&
                x.CreatedAt < fim);

            var filterTotalSpentForSingleDate = entryMovements.Where(x =>
                x.CreatedAt >= inicio ||
                x.CreatedAt < fim);

            var filterTotalSoldFromDateToDate = exitMovements.Where(x =>
                x.CreatedAt >= inicio &&
                x.CreatedAt < fim);

            var filterTotalSoldForSingleDate = exitMovements.Where(x =>
                x.CreatedAt >= inicio ||
                x.CreatedAt < fim);

            var totalSoldFilter = filterTotalSoldFromDateToDate.Count() == 0
                ? filterTotalSoldForSingleDate
                : filterTotalSoldFromDateToDate;

            var totalSpentFilter = filterTotalSpentBetweenDates.Count() == 0
                ? filterTotalSpentForSingleDate
                : filterTotalSpentBetweenDates;

            var totalSpent = totalSpentFilter.Sum(x => x.TotalValue);
            var totalSold = totalSoldFilter.Sum(x => x.TotalValue);

            var profit = totalSold - totalSpent;
            var isProfitable = totalSold > totalSpent;

            var summaryReports = new SummaryReportsResponseDTO(
                totalSpent,
                totalSold,
                profit,
                isProfitable);

            var summary = await GetSummaryReport(
                dtoFilter,
                summaryReports,
                userId);

            return await GeneratePdf(
                userId,
                dtoFilter,
                summary);
        }

    }
}
