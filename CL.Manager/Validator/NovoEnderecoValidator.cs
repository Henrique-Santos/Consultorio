using CL.Core.Shared.ModelViews.Endereco;
using FluentValidation;

namespace CL.Manager.Validator;

public class NovoEnderecoValidator : AbstractValidator<NovoEndereco>
{
    public NovoEnderecoValidator()
    {
        RuleFor(c => c.CEP).NotEmpty().NotNull();
        RuleFor(c => c.Estado).NotEmpty();
        RuleFor(c => c.Cidade).NotEmpty().NotNull().MaximumLength(200);
        RuleFor(c => c.Logradouro).NotEmpty().NotNull().MaximumLength(200);
        RuleFor(c => c.Numero).NotEmpty().NotNull().MaximumLength(10);
        RuleFor(c => c.Complemento).MaximumLength(200);
    }
}
