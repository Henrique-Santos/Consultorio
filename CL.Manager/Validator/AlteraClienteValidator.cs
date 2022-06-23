using CL.Core.Shared.ModelViews.Cliente;
using FluentValidation;

namespace CL.Manager.Validator;

public class AlteraClienteValidator : AbstractValidator<AlteraCliente>
{
    public AlteraClienteValidator()
    {
        RuleFor(c => c.Id).NotNull().NotEmpty().GreaterThan(0);
        Include(new NovoClienteValidator()); // Aproveitando a validação do NovoClienteValidator, aproveitando a extensão.
    }
}
