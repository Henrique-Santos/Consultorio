using Serilog;

namespace CL.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        IConfigurationRoot configuration = GetConfiguration();
        ConfigurateLog(configuration); // Adicionando o logger no start da nossa aplicação
        try
        {
            Log.Information("Iniciando o WebApi");
            CreateHostBuilder(args).Build().Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Erro Catastrófico");
            throw;
        }
        finally
        {
            Log.CloseAndFlush(); // Forçando o fechamento do LOG caso a app tenha travado
        }
    }

    private static void ConfigurateLog(IConfigurationRoot configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
    }

    private static IConfigurationRoot GetConfiguration()
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{environment}.json", optional: true)
            .Build();
        return configuration;
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseSerilog() // Usando o SeriLog
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
