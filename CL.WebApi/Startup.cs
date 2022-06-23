using CL.WebApi.Configuration;

namespace CL.WebApi;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddJwtConfiguration(Configuration);

        services.AddFluentValidationConfiguration();

        services.AddAutoMapperConfiguration();

        services.AddDataBaseConfiguration(Configuration);
        
        services.AddDependencyInjectionConfiguration();

        services.AddSwaggerConfiguration();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseExceptionHandler("/error");

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseDataBaseConfiguration();

        app.UseSwaggerConfiguration();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseJwtConfiguration();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}