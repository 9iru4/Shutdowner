using System;
using System.Globalization;

namespace Shutdowner
{
    /// <summary>
    /// Тип задач
    /// </summary>
    public class TaskType
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Значение
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Конструктор типа задач
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="value">Значение</param>
        public TaskType(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }

    /// <summary>
    /// Вьюшка задач
    /// </summary>
    public class MyTaskView
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Дата 
        /// </summary>
        public DateTime DateTrigger { get; set; }

        /// <summary>
        /// Дата в строковом формате
        /// </summary>
        public string Trigger { get { return DateTrigger.ToString("G", CultureInfo.CreateSpecificCulture("ru-RU")); } }

        /// <summary>
        /// Выполнено ли
        /// </summary>
        public bool Complited { get; set; }

        /// <summary>
        /// Включено ли
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public MyTaskView()
        {

        }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="description">Описание</param>
        /// <param name="trigger">Дата</param>
        /// <param name="complite">Выполнено ли</param>
        /// <param name="enable"> Включено ли</param>
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
