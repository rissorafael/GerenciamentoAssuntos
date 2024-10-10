using FluentValidation;
using GerenciamentoAssuntos.Domain.Models;

namespace GerenciamentoAssuntos.Domain.Validators
{
    public class AssuntosValidator : AbstractValidator<AssuntoRequestModel>
    {
        public AssuntosValidator()
        {
            RuleFor(x => x.Titulo)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(x => x.PalavrasChaves)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido");
        }
    }
}
