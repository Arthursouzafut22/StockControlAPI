using ControleMercadoria.Core.DTOs.Products;
using FluentValidation;

namespace ControleMercadoria.Core.Validators.Products
{
    public class CreateProductDTOValidator : AbstractValidator<CreateProductDTO>
    {
        public CreateProductDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("O nome do produto é obrigatório.");

            RuleFor(x => x.PriceCost).NotEmpty().WithMessage("Preço de custo é obrigatório.")
                .GreaterThan(0).WithMessage("Preço de custo deve ser maior que zero.")
                .Must(x => x >= 0).WithMessage("Preço de custo não pode ser negativo.");

            RuleFor(x => x.SalePrice).NotEmpty().WithMessage("Preço de venda é obrigatório.")
                .GreaterThan(0).WithMessage("Preço de venda deve ser maior que zero.")
                .Must(x => x >= 0).WithMessage("Preço de venda não pode ser negativo.");

            RuleFor(x => x.StockQuantity).NotEmpty().WithMessage("Quantidade do produto é obrigatório.")
                .GreaterThan(0).WithMessage("Quantidade do produto deve ser maior que zero.")
                .Must(x => x >= 0).WithMessage("Quantidade do produto não pode ser negativo.");
        }
    }
}
