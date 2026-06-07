using ControleMercadoria.Core.DTOs.Users;
using FluentValidation;

namespace ControleMercadoria.Core.Validators.Users
{
    public class CreateUserDTOValidator : AbstractValidator<CreateUserDTO>
    {
        public CreateUserDTOValidator()
        {
            RuleFor(x => x.Nome).NotEmpty().WithMessage("O nome é obrigatório.")
                .Matches(@"^[a-zA-ZÀ-ÿ\s]+$").WithMessage("O nome deve conter apenas letras.");

            RuleFor(x => x.Email).NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("O e-mail informado não é válido.");

            RuleFor(x => x.Senha).NotEmpty().WithMessage("A senha é obrigatória.")
                .MinimumLength(6).WithMessage("A senha deve ter pelo menos 6 caracteres.");
        }
    }
}
