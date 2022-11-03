using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Fibonacci.Common.Logging
{
    public static class LogConfigurationExtensions
    {
        public static LoggerConfiguration ReadFromSettings(this LoggerConfiguration loggerConfig, IConfiguration config)
        {
            var serilogSection = config.GetSection("Serilog");

            var filteredConfiguration = new ConfigurationBuilder()
                .AddInMemoryCollection(Enumerable.Empty<KeyValuePair<string, string>>())
                .Build();

            var targetSection = filteredConfiguration.GetSection(serilogSection.Key);

            const string writeToKey = "WriteTo";
            foreach (var childSection in serilogSection.GetChildren())
            {
                if (childSection.Key == writeToKey)
                {
                    var enabledSinks = childSection.GetChildren()
                        .Where(sink => sink.GetValue<bool>("Enabled"))
                        .ToList();

                    var writeToSection = targetSection.GetSection(writeToKey);
                    foreach (var enabledSink in enabledSinks)
                    {
                        CopySettings(writeToSection, enabledSink);
                    }
                }
                else
                {
                    CopySettings(targetSection, childSection);
                }
            }

            return loggerConfig.ReadFrom.Configuration(filteredConfiguration);
        }

        private static void CopySettings(IConfigurationSection targetSection, IConfigurationSection sourceSection)
        {
            if (sourceSection.Value != null)
            {
                targetSection[sourceSection.Key] = sourceSection.Value;
                return;
            }

            targetSection = targetSection.GetSection(sourceSection.Key);
            foreach (var childSection in sourceSection.GetChildren())
            {
                CopySettings(targetSection, childSection);
            }
        }
    }
}
