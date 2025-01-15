using OBD.FileService.DataAccess.Integration;
using OBD.FileService.Users.Intergration;
using OBD.FIleService.Service.Middlewares;
using OBD.FIleService.Service.Common.Extensions;
using OBD.FileService.Files.Integration;

namespace OBD.FIleService.Service;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = ConfigureApp(args);
        await RunApp(builder);
    }

    private static async Task RunApp(WebApplicationBuilder builder)
    {
        var app = builder.Build();
        var configuration = builder.Configuration;

        var appName = configuration["ServiceName"] 
            ?? throw new ArgumentNullException(configuration["ServiceName"], "ServiceName is not provided");

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
        }

        app.UseRouting()
           .UseCors("AllowAllOrigins")
           .UseMiddleware<BlacklistTokenMiddleware>()
           .UseAuthentication()
           .UseAuthorization();

        app.MapControllers();
        app.MapHealthChecks("/health");
        app.MapGet(string.Empty, async ctx => await ctx.Response.WriteAsync(appName));

        await app.RunAsync();
    }

    private static WebApplicationBuilder ConfigureApp(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Logging.ClearProviders();

        var services = builder.Services;
        var cfg = builder.Configuration;

        services.AddSwagger();

        services.AddControllers();
        services.AddHealthChecks();

        services.AddCorsDefault()
                .AddJwtBearerAuthentication(cfg)
                .AddAuthorizationDefault();

        ConfigureDI(services, cfg);

        return builder;
    }

    private static void ConfigureDI(IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddLogging(cfg => cfg.AddConsole());

        services.AddFilesModule()
                .AddDataAccessModule(configuration);
    }
}
