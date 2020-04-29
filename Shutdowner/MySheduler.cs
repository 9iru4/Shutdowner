using Microsoft.Win32.TaskScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shutdowner
{

    public class MySheduler
    {
        List<Microsoft.Win32.TaskScheduler.Task> AllTasks;

        public List<MyTaskView> MyTasks = new List<MyTaskView>();

        public MySheduler()
        {
            CreateTaskFolder();
            LoadTasks();
        }

        void CreateTaskFolder()
        {
            using (TaskService taskService = new TaskService())
                if (taskService.RootFolder.SubFolders.Where(x => x.Name == @"Shutdowner").FirstOrDefault() == null)
                {
                    taskService.RootFolder.CreateFolder(@"\Shutdowner");
                }
        }

        public void LoadTasks()
        {
            foreach (var item in MyTasks.ToList())
            {
                MyTasks.Remove(item);
            }
            using (TaskService taskService = new TaskService())
                AllTasks = new List<Microsoft.Win32.TaskScheduler.Task>(taskService.RootFolder.SubFolders.Where(x => x.Name == @"Shutdowner").FirstOrDefault().AllTasks);
            foreach (var task in AllTasks)
            {
                MyTasks.Add(new MyTaskView(task.Name, task.Definition.RegistrationInfo.Description, task.Definition.Triggers[0].StartBoundary, task.LastTaskResult == 0 ? true : false, task.Enabled));
            }
        }

        public void RemoveTask(Microsoft.Win32.TaskScheduler.Task task)
        {
            using (TaskService ts = new TaskService())
            {
                ts.RootFolder.SubFolders.Where(x => x.Name == @"Shutdowner").FirstOrDefault().DeleteTask(task.Name);
            }
        }

        public void ChangeTaskStatus(MyTaskView task)
        {
            using (TaskService ts = new TaskService())
            {
                ts.RootFolder.SubFolders.Where(x => x.Name == @"Shutdowner").FirstOrDefault().AllTasks.Where(x => x.Name == task.Name).FirstOrDefault().Enabled = false;
            }
        }

        public void CreateNewTaskAtTime(string description, string app, string arguments, DateTime time)
        {
            string taskDate = time.Day + "-" + time.Month + "-" + time.Year + "_" + time.Hour + "-" + time.Minute + "-" + time.Second;
            using (TaskService ts = new TaskService())
            {
                // Create a new task definition and assign properties
                TaskDefinition td = ts.NewTask();
                td.RegistrationInfo.Description = description;

                // Create a trigger that will fire the task at this time every other day
                td.Triggers.Add(new TimeTrigger(time));

                // Create an action that will launch Notepad whenever the trigger fires
                td.Actions.Add(new ExecAction(app, arguments, null));

                // Register the task in the root folder
                ts.RootFolder.RegisterTaskDefinition(@"\Shutdowner\Task" + taskDate, td);
            }
        }
    }
}
