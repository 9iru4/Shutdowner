using System;
using System.Windows.Threading;

namespace Shutdowner
{
    /// <summary>
    /// Таймер
    /// </summary>
    class MyCountDownTimer
    {
        /// <summary>
        /// Делегат
        /// </summary>
        public delegate void TimeChangedHandler();
        /// <summary>
        /// Событие тика
        /// </summary>
        public event TimeChangedHandler TimerTick;
        /// <summary>
        /// Событие остановки
        /// </summary>
        public event TimeChangedHandler TimerStop;
        /// <summary>
        /// Таймер
        /// </summary>
        DispatcherTimer timer;
        /// <summary>
        /// Секунды
        /// </summary>
        int TotalSeconds = 0;

        /// <summary>
        /// Конструктор
        /// </summary>
        public MyCountDownTimer()
        {
        }

        /// <summary>
        /// Запущен ли таймер
        /// </summary>
        /// <returns>Да\нет</returns>
        public bool IsTimerStarted()
        {
            if (timer == null) return false;
            else return true;
        }

        /// <summary>
        /// Запуск таймера
        /// </summary>
        /// <param name="seconds">Количество секунд</param>
        public void StartTimer(int seconds)
        {
            TotalSeconds = seconds;
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);

            timer.Start();
        }

        /// <summary>
        /// Остановка таймера
        /// </summary>
        public void StopTimer()
        {
            timer.Stop();
        }

        /// <summary>
        /// Событие тика таймера
        /// </summary>
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (TotalSeconds - 1 >= 0)
                TotalSeconds--;
            if (TotalSeconds == 0)
            {
                timer.Stop();
                TimerStop.Invoke();
            }
            TimerTick.Invoke();
        }

        /// <summary>
        /// Преобразование количества секунд в формат для таймера
        /// </summary>
        /// <param name="totalSeconds">Количество секунд</param>
        /// <returns>Формат для таймера Ч Ч М М С С</returns>
        public static string GetTime(int totalSeconds)
        {
            string result = "";

            var hours = (totalSeconds / 60 / 60 / 60).ToString();
            var minutes = ((totalSeconds - int.Parse(hours) * 60 * 60) / 60).ToString();
            var seconds = (totalSeconds - int.Parse(hours) * 60 * 60 - int.Parse(minutes) * 60).ToString();

            if (hours.Length > 1)
            {
                result += hours[0] + " " + hours[1] + " ";
            }
            else result += "0 " + hours + " ";

            if (minutes.Length > 1)
            {
                result += minutes[0] + " " + minutes[1] + " ";
            }
            else result += "0 " + minutes + " ";

            if (seconds.Length > 1)
            {
                result += seconds[0] + " " + seconds[1] + " ";
            }
            else result += "0 " + seconds;

            return result;
        }

        /// <summary>
        /// Преобразование количества секунд в формат для таймера
        /// </summary>
        /// <returns>Формат для таймера Ч Ч М М С С</returns>
        public string GetTime()
        {
            string result = "";

            var hours = (TotalSeconds / 60 / 60 / 60).ToString();
            var minutes = ((TotalSeconds - int.Parse(hours) * 60 * 60) / 60).ToString();
            var seconds = (TotalSeconds - int.Parse(hours) * 60 * 60 - int.Parse(minutes) * 60).ToString();

            if (hours.Length > 1)
            {
                result += hours[0] + " " + hours[1] + " ";
            }
            else result += "0 " + hours + " ";

            if (minutes.Length > 1)
            {
                result += minutes[0] + " " + minutes[1] + " ";
            }
            else result += "0 " + minutes + " ";

            if (seconds.Length > 1)
            {
                result += seconds[0] + " " + seconds[1] + " ";
            }
            else result += "0 " + seconds;

            return result;
        }
    }
}
