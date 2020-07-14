using ActivityTagger.Interfaces;
using ActivityTagger.Startup;
using ActivityTaggers;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Builder;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: WebJobsStartup(typeof(FunctionStartup))]
namespace ActivityTagger.Startup
{
    public class FunctionStartup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            builder.Services.AddSingleton<ITelemetryInitializer, TelemetryInitializer>();
            builder.Services.AddSingleton<IActivityTagger, Services.ActivityTagger>();
        }
    }
}
