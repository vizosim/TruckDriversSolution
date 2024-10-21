using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TruckDriversFunctionApp.Application;
using TruckDriversFunctionApp.Persistence;
using TruckDriversFunctionApp.Persistence.Configuration;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication(builder =>
    {
        // builder.UseMiddleware<ExceptionHandlingMiddlaware>();
        builder.Services.AddOptions<TruckDriverStoreDatabaseSettings>()
        .Configure<IConfiguration>((settings, configuration) =>
        {
            configuration.GetSection("TruckStoreDatabase").Bind(settings);
        });
    })
    .ConfigureServices(services =>
    {
        services.AddScoped<ITruckDriverRepository, TruckDriverRepository>();
        services.AddScoped<ITruckDriverApplicationQueryService, TruckDriverApplicationQueryService>();
        services.AddScoped<ITruckDriverApplicationCreateService, TruckDriverApplicationCreateService>();
    })
    .Build();

host.Run();