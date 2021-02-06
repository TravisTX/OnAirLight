using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace OnAirLight
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Debug)
                .WriteTo.File("onair.log", restrictedToMinimumLevel: LogEventLevel.Information, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7)
                .CreateLogger();

            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .UseSerilog()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<OnAir>();
                })
                .Build().Run();
        }
    }
}
