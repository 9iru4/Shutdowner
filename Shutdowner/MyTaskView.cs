using System;
using System.Globalization;
using System.Security.Cryptography;

namespace Shutdowner
{

    public class TaskType
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public TaskType(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }

    public class MyTaskView
    {
        public string Name { get; set; }
        public string Description { get; set; }

        DateTime DateTrigger { get; set; }

        public string Trigger
        {
            get
            {
                return DateTrigger.ToString("g", CultureInfo.CurrentCulture);
            }
        }

        public bool Complited { get; set; }
        public bool Enabled { get; set; }

        public MyTaskView()
        {

        }

        public MyTaskView(string name, string description, DateTime trigger, bool complite, bool enable)
        {
            Name = name;
            Description = description;
            DateTrigger = trigger;
            Complited = complite;
            Enabled = enable;
        }
    }
}
