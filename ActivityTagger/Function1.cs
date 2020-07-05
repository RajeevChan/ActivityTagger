using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ActivityTagger.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace ActivityLogger
{
    public class Function1
    {
        private readonly IActivityTagger _activityTagger;
        public Function1(IActivityTagger activityTagger)
        {
            _activityTagger = activityTagger;
        }
        [FunctionName("Function1")]
        public void Run([TimerTrigger("0 */3 * * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            
            //DI based instance
            _activityTagger.AddTag("DIOne", "Yes");

            //Direct instance
            Activity.Current?.AddTag("FromFunction", "Yes");
        }
    }
}
