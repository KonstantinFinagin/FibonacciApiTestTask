using Autofac;
using Fibonacci.Api.Bll.Mapping;
using Fibonacci.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sieve.Models;

namespace Fibonacci.Api
{
    public class Startup : BaseStartup
    {
        public Startup(IConfiguration configuration) : base(configuration)
        {
            Configuration = configuration;
        }

        public override string ServiceName => "Fibonacci API";
        public override string Description => "Provides services for Fibonacci Number calculation";

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            base.Configure(app, env);
        }

        protected override void ConfigureDependencyInjections(ContainerBuilder builder)
        {
            base.ConfigureDependencyInjections(builder);

            builder.RegisterModule<FibonacciServiceModule>();
        }

        protected override void ConfigureDataAccess(IServiceCollection services)
        {
            // TODO register DB of needed
        }

        protected override void ConfigureModelsMapping(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(FibonacciMappingProfile));
        }

        protected override void ConfigureFilterProcessor(IServiceCollection services)
        {
            services.Configure<SieveOptions>(Configuration.GetSection("Sieve"));
        }
    }
}
