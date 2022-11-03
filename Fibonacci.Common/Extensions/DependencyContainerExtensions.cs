using Autofac.Builder;
using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using AspNetOptions = Microsoft.Extensions.Options.Options;

namespace Fibonacci.Common.Extensions
{
    public static class DependencyContainerExtensions
    {
        /* no DB is used here
        public static IServiceCollection RegisterDb<T>(
            this IServiceCollection services,
            IConfiguration configuration,
            string schemaName,
            string connectionStringName = "DefaultConnection")
            where T : BaseDbContext
        {
                services.AddDbContextPool<T>(options =>
                    options.UseSqlServer(configuration.GetConnectionString(connectionStringName),
                        builder =>
                        {
                            builder.EnableRetryOnFailure(FailureRetries);
                            builder.MigrationsHistoryTable(MigrationsHistoryTableName, schemaName);
                        }));

                return services;
        }

        public static ContainerBuilder RegisterDb<T>(
            this ContainerBuilder builder,
            string schemaName,
            string connectionStringName = "DefaultConnection")
            where T : BaseDbContext
        {
            builder.Register(c =>
            {
                var configuration = c.Resolve<IConfiguration>();
                var loggerFactory = c.Resolve<ILoggerFactory>();

                var connectionString = configuration.GetConnectionString(connectionStringName);
                var dbContextOptionsBuilder = new DbContextOptionsBuilder<T>()
                    .UseLoggerFactory(loggerFactory)
                    .UseSqlServer(connectionString,
                        dbBuilder =>
                        {
                            dbBuilder.EnableRetryOnFailure(FailureRetries);
                            dbBuilder.MigrationsHistoryTable(MigrationsHistoryTableName, schemaName);
                        });

                return dbContextOptionsBuilder.Options;
            })
                .AsSelf()
                .SingleInstance();

            builder.Register(context => context.Resolve<DbContextOptions<T>>())
                .As<DbContextOptions>()
                .SingleInstance();

            builder.RegisterType<T>()
                .AsSelf()
                .InstancePerLifetimeScope();

            return builder;
        }
        */

        public static IRegistrationBuilder<TOptions, SimpleActivatorData, SingleRegistrationStyle> RegisterConfiguration<TOptions>(this ContainerBuilder builder, string sectionName)
            where TOptions : class, new()
        {
            builder.Register(c => new ConfigurationChangeTokenSource<TOptions>(AspNetOptions.DefaultName, c.Resolve<IConfiguration>().GetSection(sectionName)))
                .As<IOptionsChangeTokenSource<TOptions>>()
                .SingleInstance();

            builder.Register(c => new NamedConfigureFromConfigurationOptions<TOptions>(AspNetOptions.DefaultName, c.Resolve<IConfiguration>().GetSection(sectionName)))
                .As<IConfigureOptions<TOptions>>()
                .SingleInstance();

            return builder.Register(c => c.Resolve<IOptions<TOptions>>().Value)
                .As<TOptions>()
                .SingleInstance();
        }
    }
}
