using CL.Core.Shared.ModelViews.Medico;
using CL.Manager.Interfaces.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CL.Manager.Validator
{
    public class AlteraMedicoValidator : AbstractValidator<AlteraMedico>
    {
        public AlteraMedicoValidator(IEspecialidadeRepository repository)
        {
            RuleFor(c => c.Id).NotNull().NotEmpty().GreaterThan(0);
            Include(new NovoMedicoValidator(repository));
        }
    }
}
