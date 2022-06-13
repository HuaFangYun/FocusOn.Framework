
using Boloni.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Serilog.Events;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
#if DEBUG
        .MinimumLevel.Override("Boloni", LogEventLevel.Debug)
#else
                .MinimumLevel.Override("Boloni", LogEventLevel.Information)
#endif
        .Enrich.FromLogContext()
    .WriteTo.Async(c => c.File("Logs/logs.txt"))
    .WriteTo.Async(c => c.Console())
    .CreateLogger();

var build = Host.CreateDefaultBuilder(args)
            .ConfigureLogging((context, logging) => logging.ClearProviders())
            .ConfigureServices((context, services) =>
            {
                services.AddDbContext<BoloniDbContext>(configure =>
                {
                    configure.UseSqlServer(context.Configuration.GetConnectionString("Default"));
                });
            })
            ;

Console.WriteLine("Starting migration");
//await build.Build().Services.GetRequiredService<BoloniDbContext>().Database.MigrateAsync();

await build.StartAsync();