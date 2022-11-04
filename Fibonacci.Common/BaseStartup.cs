using System;
using System.IO;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using Autofac;
using Fibonacci.Common.Documentation.Filters;
using Fibonacci.Common.Exceptions;
using Fibonacci.Common.Modules;
using Fibonacci.Common.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Http;
using Microsoft.OpenApi.Models;

namespace Fibonacci.Common
{
    public abstract class BaseStartup
    {
        protected BaseStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        public abstract string ServiceName { get; }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCompression();

            ConfigureDataAccess(services);
            ConfigureDocumentation(services);

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IncludeFields = true;
                options.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddHttpContextAccessor();
            services.AddHttpClient();

            services.AddHealthChecks();

            // remove default logging from http client
            services.RemoveAll<IHttpMessageHandlerBuilderFilter>();

            ConfigureModelsMapping(services);
            ConfigureServiceCollection(services);
            ConfigureFilterProcessor(services);
        }

        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();
            app.UseCustomExceptions();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{ServiceName} Api");
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new EmbeddedFileProvider(Assembly.GetEntryAssembly())
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new DefaultModule());
            ConfigureDependencyInjections(builder);
        }

        private void ConfigureDocumentation(IServiceCollection services)
        {
            var startupAssembly = GetType().Assembly;
            var assemblyLocation = Path.GetDirectoryName(startupAssembly.Location);

            var xmlCommentsFile = $"{Path.Combine(assemblyLocation, startupAssembly.GetName().Name)}.xml";
            if (!File.Exists(xmlCommentsFile))
            {
                throw new Exception($"Cannot find XML comments file {xmlCommentsFile}");
            }

            services.AddTransient<IApiDescriptionProvider, ApiExplorerDescriptionProvider>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = ServiceName,
                    Description = $"<p><strong>Build: </strong>{Configuration["BuildNumber"]}</p>",
                    Version = "v1"
                });
                c.DocumentFilter<HideRpcEndpointsDocumentFilter>();
            });
        }

        protected virtual void ConfigureDataAccess(IServiceCollection services)
        {
            // Default impl
        }

        protected virtual void ConfigureDependencyInjections(ContainerBuilder builder)
        {
            // Default impl
        }

        protected virtual void ConfigureModelsMapping(IServiceCollection services)
        {
            // Default impl
        }

        protected virtual void ConfigureServiceCollection(IServiceCollection services)
        {
            // Default impl
        }

        protected virtual void ConfigureFilterProcessor(IServiceCollection services)
        {
            // default impl
        }
    }

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCustomExceptions(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
