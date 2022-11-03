using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using Fibonacci.Common.Logging;
using Microsoft.AspNetCore.Hosting;

namespace Fibonacci.Common
{
    public class WebHostRunner<TStartup>
        where TStartup : class
    {
        private readonly string _environmentName;
        private IConfiguration _configuration;
        private ILogger _logger;

        public WebHostRunner(string serviceName)
        {
            ServiceName = serviceName;

            _environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            Initialize();
            BuildWebHost();
        }
        public string ServiceName { get; }

        public IHostBuilder HostBuilder { get; private set; }

        public void Run()
        {
            StartWebHost();
        }

        private void BuildWebHost()
        {
            try
            {
                _logger.Debug("{ServiceName} creating host", ServiceName);

                HostBuilder = GetHostBuilder();

                _logger.Debug("{ServiceName} host created", ServiceName);
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex, "{ApplicationName} host creation failed", ServiceName);
                Log.CloseAndFlush();
                throw;
            }
        }

        private IHostBuilder GetHostBuilder()
        {
            return Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webHostBuilder =>
                {
                    webHostBuilder
                        .UseShutdownTimeout(TimeSpan.FromSeconds(10))
                        .UseSetting(WebHostDefaults.DetailedErrorsKey, "true")
                        .UseKestrel(x => x.AddServerHeader = false)
                        .UseConfiguration(_configuration)
                        .UseStartup<TStartup>();
                })
                .UseSerilog(Log.Logger);
        }

        private void StartWebHost()
        {
            try
            {
                HostBuilder.Start(); // this doesn't block the thread

            }
            catch (Exception ex)
            {
                _logger.Fatal(ex, "{ApplicationName} host start failed", ServiceName);
                Log.CloseAndFlush();
                throw;
            }
        }

        private void Initialize()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{_environmentName}.json", optional: true)
                .AddEnvironmentVariables();
            _configuration = builder
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFromSettings(_configuration)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Environment", _environmentName)
                .Enrich.WithProperty("ApplicationName", ServiceName)
                .CreateLogger();

            _logger = Log.ForContext<WebHostRunner<TStartup>>();
        }
    }
}
