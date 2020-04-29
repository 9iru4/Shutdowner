using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutdowner
{
    public class MyTask
    {
        public string Description { get; set; }
        public string AppName { get; set; }

        public string Arguments { get; set; }

        public DateTime Trigger { get; set; }

        public MyTask()
        {

        }

        public MyTask(string description, string appName, string arguments, DateTime trigger)
        {
            Description = description;
            AppName = appName;
            Arguments = arguments;
            Trigger = trigger;
        }
    }
}
