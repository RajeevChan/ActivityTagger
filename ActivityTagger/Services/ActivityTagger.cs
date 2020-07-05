using ActivityTagger.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ActivityTagger.Services
{
    public class ActivityTagger : IActivityTagger
    {
        public void AddTag(string key, string value)
        {
            Activity.Current?.AddTag(key, value);
        }
    }
}
