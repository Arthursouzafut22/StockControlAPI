using ControleMercadoria.Application.Services.Movements;
using ControleMercadoria.Application.Services.Products;
using ControleMercadoria.Core.DTOs.Reports;

namespace ControleMercadoria.Application.Services.Reports
{
    public class ReportsService : IReportsService
    {
        private readonly IProductService _serviceProduct;
        private readonly IMovementService _serviceMovement;
        public ReportsService(IProductService serviceProduct,
            IMovementService serviceMovement)
        {
            _serviceProduct = serviceProduct;
            _serviceMovement = serviceMovement;
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
    }
}
