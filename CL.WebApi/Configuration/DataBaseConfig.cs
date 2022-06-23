using CL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CL.WebApi.Configuration;

public static class DataBaseConfig
{
    public static void AddDataBaseConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CLContext>(options => options.UseSqlServer(configuration.GetConnectionString("CLConnection")));
    }

    public static void UseDataBaseConfiguration(this IApplicationBuilder app)
    {
        /* Na classe Startup, não é possivel acessar nossos serviços com a injeção de dependencia. 
           Pra isso, acessamos o ServiceScopeFactory do .NET, que nos disponibiliza o objeto.
        */
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        using var context = serviceScope.ServiceProvider.GetRequiredService<CLContext>(); // Obtem contexto do BD
        context.Database.Migrate(); // Aplica migrações pendentes
        context.Database.EnsureCreated(); // Garante que o BD exista
    }
}
