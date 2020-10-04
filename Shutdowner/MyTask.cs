using System;

namespace Shutdowner
{
    /// <summary>
    /// Задание
    /// </summary>
    public class MyTask
    {
        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Приложение для запуска
        /// </summary>
        public string AppName { get; set; }
        /// <summary>
        /// Аргументы
        /// </summary>
        public string Arguments { get; set; }
        /// <summary>
        /// Дата
        /// </summary>
        public DateTime Trigger { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public MyTask()
        {

        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="description">Описание</param>
        /// <param name="appName">Название приложения</param>
        /// <param name="arguments">Аргументы</param>
        /// <param name="trigger">Дата</param>
        public MyTask(string description, string appName, string arguments, DateTime trigger)
        {
            Description = description;
            AppName = appName;
            Arguments = arguments;
            Trigger = trigger;
        }
    }
}
