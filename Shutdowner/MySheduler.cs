using Microsoft.Win32.TaskScheduler;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Shutdowner
{
    /// <summary>
    /// Планировщик
    /// </summary>
    public class MySheduler
    {
        /// <summary>
        /// Список заданий этой программы
        /// </summary>
        public ObservableCollection<MyTaskView> MyTasks = new ObservableCollection<MyTaskView>();

        /// <summary>
        /// Конструктор с базовой инициализацией
        /// </summary>
        public MySheduler()
        {
            CreateTaskFolder();
            LoadTasks();
        }

        /// <summary>
        /// Создание папки в планировщике
        /// </summary>
        void CreateTaskFolder()
        {
            using (TaskService taskService = new TaskService())
                if (taskService.RootFolder.SubFolders.Where(x => x.Name == @"Shutdowner").FirstOrDefault() == null)//Если не существует
                {
                    taskService.RootFolder.CreateFolder(@"\Shutdowner");
                }
        }

        /// <summary>
        /// Загрузка заданий из планировщика
        /// </summary>
        public void LoadTasks()
        {
            using (TaskService taskService = new TaskService())
            {
                foreach (var task in taskService.RootFolder.SubFolders.Where(x => x.Name == @"Shutdowner").FirstOrDefault().AllTasks)
                {
                    var myTask = MyTasks.Where(x => x.Name == task.Name).FirstOrDefault();
                    if (myTask != null)
                        MyTasks.Remove(myTask);
                    MyTasks.Add(new MyTaskView(task.Name, task.Definition.RegistrationInfo.Description, task.Definition.Triggers[0].StartBoundary, task.IsActive, task.Enabled));
                }
            }
        }

        /// <summary>
        /// Удаление задания
        /// </summary>
        /// <param name="task">Задание</param>
        public void RemoveTask(MyTaskView task)
        {
            MyTasks.Remove(task);
            using (TaskService ts = new TaskService())
            {
                ts.RootFolder.SubFolders.Where(x => x.Name == @"Shutdowner").FirstOrDefault().DeleteTask(task.Name);
            }
        }

        /// <summary>
        /// Изменение статуса задачи
        /// </summary>
        /// <param name="task">Задание</param>
        public void DisableTaskStatus(MyTaskView task)
        {
            using (TaskService ts = new TaskService())
            {
                ts.RootFolder.SubFolders.Where(x => x.Name == @"Shutdowner").FirstOrDefault().AllTasks.Where(x => x.Name == task.Name).FirstOrDefault().Enabled = false;
                MyTasks.Where(x => x.Name == task.Name).FirstOrDefault().Enabled = false;
            }
        }

        /// <summary>
        /// Создание задачи
        /// </summary>
        /// <param name="description">Описание</param>
        /// <param name="app">Программа</param>
        /// <param name="arguments">Аргументы</param>
        /// <param name="time">Время</param>
        public void CreateNewTaskAtTime(string description, string app, string arguments, DateTime time)
        {
            //Имя задания 
            string taskName = @"\Shutdowner\Task " + time.Day + "-" + time.Month + "-" + time.Year + "_" + time.Hour + "-" + time.Minute + "-" + time.Second;
            using (TaskService ts = new TaskService())
            {
                // Создание нового описания задания
                TaskDefinition td = ts.NewTask();
                td.RegistrationInfo.Description = description;

                td.Principal.RunLevel = TaskRunLevel.Highest;

                // Добавление тригера срабатывания
                td.Triggers.Add(new TimeTrigger(time));

                // Добавление действия
                td.Actions.Add(new ExecAction(app, arguments, null));

                // Регистрация задания в плнировщике
                var task = ts.RootFolder.RegisterTaskDefinition(taskName, td);

                //Добавление задания в список
                MyTasks.Add(new MyTaskView(task.Name, task.Definition.RegistrationInfo.Description, task.Definition.Triggers[0].StartBoundary, task.LastTaskResult == 0 ? true : false, task.Enabled));
            }
        }
    }
}
