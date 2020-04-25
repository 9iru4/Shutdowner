using System;
using System.Windows.Threading;

namespace Shutdowner
{
    class MyCountDownTimer
    {
        public delegate void TimeChangedHandler();
        public event TimeChangedHandler TimerTick;
        public event TimeChangedHandler TimerStop;
        DispatcherTimer timer;
        int TotalSeconds = 0;

        public MyCountDownTimer()
        {
        }

        public void StartTimer(int seconds)
        {
            TotalSeconds = seconds;
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);

            timer.Start();
        }

        public void StopTimer()
        {
            timer.Stop();
        }

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
