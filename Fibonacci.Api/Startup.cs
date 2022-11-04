using Autofac;
using Fibonacci.Api.Bll.Mapping;
using Fibonacci.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fibonacci.Api
{
    public class Startup : BaseStartup
    {
        public Startup(IConfiguration configuration) : base(configuration)
        {
            Configuration = configuration;
        }

        public override string ServiceName => "Fibonacci API";
        public virtual string Description => "Provides services for Fibonacci Number calculation";

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            base.Configure(app, env);
        }

        protected override void ConfigureDependencyInjections(ContainerBuilder builder)
        {
            base.ConfigureDependencyInjections(builder);

            builder.RegisterModule<FibonacciApiModule>();
        }

        protected override void ConfigureDataAccess(IServiceCollection services)
        {
            // TODO register DB if needed
        }

        protected override void ConfigureModelsMapping(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(FibonacciMappingProfile));
        }

        protected override void ConfigureFilterProcessor(IServiceCollection services)
        {
            // TODO Sieve Processor if needed
        }
    }
}
