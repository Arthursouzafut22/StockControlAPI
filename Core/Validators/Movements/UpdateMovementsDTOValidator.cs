using ControleMercadoria.Core.DTOs.Movements;
using FluentValidation;

namespace ControleMercadoria.Core.Validators.Movements
{
    public class UpdateMovementsDTOValidator : AbstractValidator<UpdateMovementsDTO>
    {
        public UpdateMovementsDTOValidator()
        {

            RuleFor(x => x.Amount)
                .NotEmpty().WithMessage("A Quantidade é obrigatória.")
                .GreaterThan(0).WithMessage("A Quantidade deve ser maior que zero.");

            RuleFor(x => x.UnitValue)
                .GreaterThan(0).WithMessage("O Preço Unitário deve ser maior que zero.");
        }
    }
}
