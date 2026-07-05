using ControleMercadoria.Core.DTOs.Movements;
using ControleMercadoria.Core.DTOs.Products;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ControleMercadoria.Core.DTOs.Reports;

namespace ControleMercadoria.Application.Services.Reports.Documents
{
    public class MovementReportDocument : IDocument
    {
        public IEnumerable<MovementsResponseDTO> Movements { get; set; }
        public IEnumerable<ProductResponseDTO> Products { get; set; }
        public SummaryReportsResponseDTO Summary { get; set; }
        public ReportPeriodFilterDTO Period { get; set; }

        public MovementReportDocument(
            IEnumerable<MovementsResponseDTO> movements,
            IEnumerable<ProductResponseDTO> 
            products, SummaryReportsResponseDTO summary,
            ReportPeriodFilterDTO period
            )
        {
            QuestPDF.Settings.License = LicenseType.Community;
            Movements = movements;
            Products = products;
            Summary = summary;
            Period = period;
        }

        public string GetMovementDatesByPeriod()
        {
            var period = (Period.dataInicio == null && Period.dataFim == null)
                ? "todas as movimentações"
                : Period.dataInicio != null && Period.dataFim != null
                    ? $"{Period.dataInicio} - {Period.dataFim}"
                    : Period.dataInicio != null
                        ? Period.dataInicio
                        : Period.dataFim != null
                            ? Period.dataFim
                            : string.Empty;

            return period;
        }
        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Header().Background("#1a1a1a").Padding(20).Column(col =>
                {

                    col.Item().Text("PARIS IMPORTS").FontSize(14).Bold().FontColor("#d4a853");
                    col.Item().Text("Relatório de Estoque").FontSize(24).Bold().FontColor(Colors.White);

                    col.Item().Row(row =>
                    {
                        row.RelativeItem().Text(text =>
                        {
                            text.Span("Período: ").FontColor(Colors.White).FontSize(10);
                            text.Span($"{GetMovementDatesByPeriod()}")
                            .FontColor("#d4a853").FontSize(10);
                        });

                        row.RelativeItem().AlignRight()
                           .Text($"Emitido em {DateTime.Now:dd/MM/yyyy, HH:mm:ss}")
                           .FontColor(Colors.White).FontSize(10);
                    });
                });

                page.Content().Padding(20).Column(col =>
                {
                    col.Item().PaddingBottom(8).Text("Resumo financeiro").FontSize(14).Bold();

                    col.Item().Border(0.5f).BorderColor("#e5e7eb").Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(3);
                            columns.RelativeColumn(1);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Background("#1a1a1a").Padding(8)
                                .Text("Indicador").FontColor("#d4a853").Bold().FontSize(10);

                            header.Cell().Background("#1a1a1a").Padding(8).AlignRight()
                                .Text("Valor").FontColor("#d4a853").Bold().FontSize(10);
                        });

                        table.Cell().Background("#e5e7eb").PaddingHorizontal(8).PaddingVertical(4)
                        .BorderBottom(0.5f).BorderColor("#e5e7eb")
                            .Text("Total gasto (entradas)").FontSize(10);

                        table.Cell().Background("#e5e7eb").PaddingHorizontal(8).PaddingVertical(4)
                        .BorderBottom(0.5f).BorderColor("#e5e7eb").AlignRight()
                            .Text(Summary.TotalSpent.ToString("C")).FontSize(10);

                        table.Cell().Background("#e5e7eb").PaddingHorizontal(8).PaddingVertical(4)
                        .BorderBottom(0.5f).BorderColor("#e5e7eb")
                            .Text("Total vendido (saídas)").FontSize(10);

                        table.Cell().Background("#e5e7eb").PaddingHorizontal(8).PaddingVertical(4)
                        .BorderBottom(0.5f).BorderColor("#e5e7eb").AlignRight()
                            .Text(Summary.TotalSold.ToString("C")).FontSize(10);

                        table.Cell().Background("#e5e7eb").PaddingHorizontal(8).PaddingVertical(4)
                        .BorderBottom(0.5f).BorderColor("#e5e7eb")
                            .Text("Lucro").FontSize(10);

                        table.Cell().Background("#e5e7eb").PaddingHorizontal(8).PaddingVertical(4)
                        .BorderBottom(0.5f).BorderColor("#e5e7eb").AlignRight()
                            .Text(Summary.Profit.ToString("C")).FontSize(10);

                    });

                    col.Item().PaddingBottom(8).PaddingTop(15).Text("Estoque por produto").FontSize(14).Bold();

                    col.Item().Border(0.5f).BorderColor("#e5e7eb").Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Background("#1a1a1a").PaddingHorizontal(8).PaddingVertical(10)
                                .Text("Produto").FontColor("#d4a853").Bold().FontSize(9);

                            header.Cell().Background("#1a1a1a").PaddingHorizontal(8).PaddingVertical(10)
                                .AlignCenter()
                                .Text("Estoque").FontColor("#d4a853").Bold().FontSize(9);

                            header.Cell().Background("#1a1a1a").PaddingHorizontal(8).PaddingVertical(10)
                                .AlignCenter()
                                .Text("Custo un.").FontColor("#d4a853").Bold().FontSize(9);

                            header.Cell().Background("#1a1a1a").PaddingHorizontal(8).PaddingVertical(10)
                                .AlignCenter()
                                .Text("Venda un.").FontColor("#d4a853").Bold().FontSize(9);

                            header.Cell().Background("#1a1a1a").PaddingHorizontal(8).PaddingVertical(10)
                                .AlignRight()
                                .Text("Valor total").FontColor("#d4a853").Bold().FontSize(9);
                        });

                        foreach (var product in Products)
                        {
                            table.Cell().Background("#e5e7eb").PaddingHorizontal(8).PaddingVertical(4)
                            .BorderBottom(0.5f).BorderColor("#e5e7eb")
                                .Text(product.Name).FontSize(9);

                            table.Cell().Background("#e5e7eb").PaddingHorizontal(8).PaddingVertical(4)
                            .BorderBottom(0.5f).BorderColor("#e5e7eb")
                                .AlignCenter().Text(product.StockQuantity.ToString()).FontSize(9);

                            table.Cell().Background("#e5e7eb").PaddingHorizontal(8).PaddingVertical(4)
                            .BorderBottom(0.5f).BorderColor("#e5e7eb")
                                .AlignCenter().Text(product.PriceCost.ToString("C")).FontSize(9);

                            table.Cell().Background("#e5e7eb").PaddingHorizontal(8).PaddingVertical(4)
                            .BorderBottom(0.5f).BorderColor("#e5e7eb")
                                .AlignCenter().Text(product.SalePrice.ToString("C")).FontSize(9);

                            table.Cell().Background("#e5e7eb").PaddingHorizontal(8).PaddingVertical(4)
                            .BorderBottom(0.5f).BorderColor("#e5e7eb")
                                .AlignRight().Text(product.SalePrice.ToString("C")).FontSize(9);
                        }

                    });

                    col.Item().PaddingBottom(8).PaddingTop(15).Text("Movimentações").FontSize(14).Bold();

                    col.Item().Border(0.5f).BorderColor("#e5e7eb").Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(2); 
                            columns.RelativeColumn(2); 
                            columns.RelativeColumn(1); 
                            columns.RelativeColumn(1); 
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(2);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Background("#1a1a1a").PaddingHorizontal(8).PaddingVertical(10)
                                .Text("Data").FontColor("#d4a853").Bold().FontSize(9);
                            header.Cell().Background("#1a1a1a").PaddingHorizontal(8).PaddingVertical(10)
                                .Text("Produto").FontColor("#d4a853").Bold().FontSize(9);
                            header.Cell().Background("#1a1a1a").PaddingHorizontal(8).PaddingVertical(10)
                                .Text("Tipo").FontColor("#d4a853").Bold().FontSize(9);
                            header.Cell().Background("#1a1a1a").PaddingHorizontal(8).PaddingVertical(10)
                                .AlignCenter().Text("Qtd").FontColor("#d4a853").Bold().FontSize(9);
                            header.Cell().Background("#1a1a1a").PaddingHorizontal(8).PaddingVertical(10)
                                .AlignCenter().Text("Valor un.").FontColor("#d4a853").Bold().FontSize(9);
                            header.Cell().Background("#1a1a1a").PaddingHorizontal(8).PaddingVertical(10)
                                .AlignRight().Text("Total").FontColor("#d4a853").Bold().FontSize(9);
                        });

                        foreach(var movement in Movements)
                        {
                            table.Cell().Background("#e5e7eb").PaddingHorizontal(8).PaddingVertical(4)
                            .BorderBottom(0.5f).BorderColor("#e5e7eb")
                               .Text(movement.CreatedAt.ToString("dd/MM/yyyy, HH:mm:ss")).FontSize(9);
                            table.Cell().Background("#e5e7eb").PaddingHorizontal(8).PaddingVertical(4)
                            .BorderBottom(0.5f).BorderColor("#e5e7eb")
                                .Text(movement.ProductName).FontSize(9);
                            table.Cell().Background("#e5e7eb").PaddingHorizontal(8).PaddingVertical(4)
                            .BorderBottom(0.5f).BorderColor("#e5e7eb")
                                .Text(movement.Type.ToString().ToLower()).FontSize(9);
                            table.Cell().Background("#e5e7eb").PaddingHorizontal(8).PaddingVertical(4)
                            .BorderBottom(0.5f).BorderColor("#e5e7eb")
                                .AlignCenter().Text(movement.Amount.ToString()).FontSize(9);
                            table.Cell().Background("#e5e7eb").PaddingHorizontal(8).PaddingVertical(4)
                            .BorderBottom(0.5f).BorderColor("#e5e7eb")
                                .AlignCenter().Text(movement.UnitValue.ToString("C")).FontSize(9);
                            table.Cell().Background("#e5e7eb").PaddingHorizontal(8).PaddingVertical(4)
                            .BorderBottom(0.5f).BorderColor("#e5e7eb")
                                .AlignRight().Text(movement.TotalValue.ToString("C")).FontSize(9);
                        }

                    });

                });

            });
        }
    }
}
