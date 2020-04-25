using Microsoft.Win32.TaskScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutdowner
{
    public enum TaskType
    {
        Restart, Shutdown, Hybirnate, RunApp
    }
    public class MySheduler
    {
        public List<Microsoft.Win32.TaskScheduler.Task> MyTasks;

        public MySheduler()
        {
            TaskService taskService = new TaskService();
            MyTasks = new List<Microsoft.Win32.TaskScheduler.Task>(taskService.AllTasks.Where(x => x.Name == "Shutdowner").ToList());
        }
    }
}
