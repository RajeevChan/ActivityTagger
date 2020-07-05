using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ActivityTagger.Interfaces
{
    public interface IActivityTagger
    {
        public void AddTag(string key, string value);
    }
}
