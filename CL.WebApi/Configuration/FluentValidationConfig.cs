using CL.Manager.Validator;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace CL.WebApi.Configuration
{
    public static class FluentValidationConfig
    {
        public static void AddFluentValidationConfiguration(this IServiceCollection services)
        {
            // Adicionando as classes de validação do Fluent Validator
            services.AddControllers()
                .AddFluentValidation(c =>
                {
                    c.RegisterValidatorsFromAssemblyContaining<NovoClienteValidator>();
                    c.RegisterValidatorsFromAssemblyContaining<AlteraClienteValidator>();
                    c.ValidatorOptions.LanguageManager.Culture = new CultureInfo("pt-br");
                });
        }
    }
}
