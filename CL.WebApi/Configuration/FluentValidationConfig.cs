using CL.Manager.Validator;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Globalization;

namespace CL.WebApi.Configuration
{
    public static class FluentValidationConfig
    {
        public static void AddFluentValidationConfiguration(this IServiceCollection services)
        {
            // Adicionando as classes de validação do Fluent Validator
            services.AddControllers()
                .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore) // Corrigindo erro de referencia ciclica
                .AddFluentValidation(c =>
                {
                    c.RegisterValidatorsFromAssemblyContaining<NovoClienteValidator>();
                    c.RegisterValidatorsFromAssemblyContaining<NovoEnderecoValidator>();
                    c.RegisterValidatorsFromAssemblyContaining<AlteraClienteValidator>();
                    c.RegisterValidatorsFromAssemblyContaining<NovoTelefoneValidator>();
                    c.RegisterValidatorsFromAssemblyContaining<NovoMedicoValidator>();
                    c.RegisterValidatorsFromAssemblyContaining<AlteraMedicoValidator>();
                    c.ValidatorOptions.LanguageManager.Culture = new CultureInfo("pt-BR");
                });
        }
    }
}
