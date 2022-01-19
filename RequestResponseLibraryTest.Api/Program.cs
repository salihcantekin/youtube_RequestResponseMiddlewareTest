using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.Graylog;

namespace RequestResponseLibraryTest.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Graylog(new GraylogSinkOptions()
                { 
                    HostnameOrAddress = "127.0.0.1",
                    Port = 12201,
                    TransportType = Serilog.Sinks.Graylog.Core.Transport.TransportType.Udp,
                    Facility = "RequestResponseLibraryTest"
                })
                .CreateLogger();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(opt => 
                {
                    opt.ClearProviders();
                })
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
