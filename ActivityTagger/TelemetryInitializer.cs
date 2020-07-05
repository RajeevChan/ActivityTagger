using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Resources;
using System.Text;

namespace ActivityTaggers
{
    public class TelemetryInitializer : ITelemetryInitializer
    {
        private readonly IConfiguration _config;

        public TelemetryInitializer(IConfiguration config)
        {
            _config = config;
        }
        public void Initialize(ITelemetry telemetry)
        {
            telemetry.Context.GlobalProperties.TryAdd("familyName", _config["FamilyName"]);

            var activity = Activity.Current;

            if(activity != null)
            {
                foreach(var tag in activity.Tags)
                {
                    telemetry.Context.GlobalProperties.TryAdd(tag.Key, tag.Value);
                }
            }
        }
    }
}
