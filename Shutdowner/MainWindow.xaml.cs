﻿using MahApps.Metro.Controls;
using Microsoft.Win32.TaskScheduler;
using Shutdowner.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Shutdowner
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        MyCountDownTimer timer = new MyCountDownTimer();
        MySheduler mySheduler;

        public MainWindow()
        {
            InitializeComponent();
            timer.TimerTick += Timer_TimeChangedEvent;
            timer.TimerStop += Timer_TimerStop;

            StartButton.Background = new SolidColorBrush(Color.FromRgb(65, 177, 225));

            mySheduler = new MySheduler();

            SetTimerColor(Color.FromRgb(65, 177, 225));
        }

        private void Timer_TimerStop()
        {
            StartButton.Content = "пуск";
            ShowUpDownButons();
            StartButton.Background = new SolidColorBrush(Color.FromRgb(65, 177, 225));
        }

        private void Timer_TimeChangedEvent()
        {
            SetTime(timer.GetTime());
        }

        void SetTime(string time)
        {
            var clock = time.Split(' ');
            Hours1.Text = clock[0];
            Hours2.Text = clock[1];
            Minutes1.Text = clock[2];
            Minutes2.Text = clock[3];
            Seconds1.Text = clock[4];
            Seconds2.Text = clock[5];
        }

        private void AboutButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow(this);
            aboutWindow.ShowDialog();
        }

        private void SettingButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow(this);
            settingsWindow.ShowDialog();
        }

        private void DonateButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DonateWindow donateWindow = new DonateWindow(this);
            donateWindow.ShowDialog();
        }

        void SetTimerColor(Color color)
        {
            Hours1.Background = new SolidColorBrush(color);
            Hours2.Background = new SolidColorBrush(color);
            Minutes1.Background = new SolidColorBrush(color);
            Minutes2.Background = new SolidColorBrush(color);
            Seconds1.Background = new SolidColorBrush(color);
            Seconds2.Background = new SolidColorBrush(color);
            Divide1.Foreground = new SolidColorBrush(color);
            Divide2.Foreground = new SolidColorBrush(color);

            Seconds1ButtonUp.Background = new SolidColorBrush(color);
            Seconds1ButtonDown.Background = new SolidColorBrush(color);
            Seconds2ButtonUp.Background = new SolidColorBrush(color);
            Seconds2ButtonDown.Background = new SolidColorBrush(color);

            Minutes1ButtonUp.Background = new SolidColorBrush(color);
            Minutes1ButtonDown.Background = new SolidColorBrush(color);
            Minutes2ButtonUp.Background = new SolidColorBrush(color);
            Minutes2ButtonDown.Background = new SolidColorBrush(color);

            Hours1ButtonUp.Background = new SolidColorBrush(color);
            Hours1ButtonDown.Background = new SolidColorBrush(color);
            Hours2ButtonUp.Background = new SolidColorBrush(color);
            Hours2ButtonDown.Background = new SolidColorBrush(color);

            TaskTypeButton.Background = new SolidColorBrush(color);
        }

        void HideUpDownButtons()
        {
            Seconds1ButtonUp.Visibility = Visibility.Hidden;
            Seconds1ButtonDown.Visibility = Visibility.Hidden;
            Seconds2ButtonUp.Visibility = Visibility.Hidden;
            Seconds2ButtonDown.Visibility = Visibility.Hidden;

            Minutes1ButtonUp.Visibility = Visibility.Hidden;
            Minutes1ButtonDown.Visibility = Visibility.Hidden;
            Minutes2ButtonUp.Visibility = Visibility.Hidden;
            Minutes2ButtonDown.Visibility = Visibility.Hidden;

            Hours1ButtonUp.Visibility = Visibility.Hidden;
            Hours1ButtonDown.Visibility = Visibility.Hidden;
            Hours2ButtonUp.Visibility = Visibility.Hidden;
            Hours2ButtonDown.Visibility = Visibility.Hidden;

            TaskTypeButton.Visibility = Visibility.Hidden;
        }

        void ShowUpDownButons()
        {
            Seconds1ButtonUp.Visibility = Visibility.Visible;
            Seconds1ButtonDown.Visibility = Visibility.Visible;
            Seconds2ButtonUp.Visibility = Visibility.Visible;
            Seconds2ButtonDown.Visibility = Visibility.Visible;

            Minutes1ButtonUp.Visibility = Visibility.Visible;
            Minutes1ButtonDown.Visibility = Visibility.Visible;
            Minutes2ButtonUp.Visibility = Visibility.Visible;
            Minutes2ButtonDown.Visibility = Visibility.Visible;

            Hours1ButtonUp.Visibility = Visibility.Visible;
            Hours1ButtonDown.Visibility = Visibility.Visible;
            Hours2ButtonUp.Visibility = Visibility.Visible;
            Hours2ButtonDown.Visibility = Visibility.Visible;

            TaskTypeButton.Visibility = Visibility.Visible;
        }

        void AbortShutdown()
        {
            ProcessStartInfo procStartInfo = new ProcessStartInfo("cmd", "/c shutdown /a")
            {
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process proc = new Process())
            {
                proc.StartInfo = procStartInfo;
                proc.Start();
            }
        }

        void CreateNewTaskAtTime(string description, string app, string arguments, DateTime time)
        {
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
                ts.RootFolder.RegisterTaskDefinition(@"Shutdowner", td);
            }
        }

        int GetSeconds()
        {
            return int.Parse(Hours1.Text + Hours2.Text) * 60 * 60 + int.Parse(Minutes1.Text + Minutes2.Text) * 60 + int.Parse(Seconds1.Text + Seconds2.Text);
        }

        private void StartButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (GetSeconds() > 0)
                if (StartButton.Content.ToString() == "пуск")
                {
                    StartButton.Content = "стоп";
                    HideUpDownButtons();
                    StartButton.Background = new SolidColorBrush(Colors.Red);
                    timer.StartTimer(GetSeconds());
                    CreateNewTaskAtTime("Запланированное выключение", "shutdown.exe", "/s /t 1000", DateTime.Now.AddSeconds(GetSeconds() + 1));

                }
                else
                {
                    StartButton.Content = "пуск";
                    ShowUpDownButons();
                    StartButton.Background = new SolidColorBrush(Color.FromRgb(65, 177, 225));
                    timer.StopTimer();
                }
            else MessageBox.Show("установите таймер");
        }

        private void Seconds2ButtonDown_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if ((int.Parse(Seconds2.Text) - 1) > 0)
                Seconds2.Text = (int.Parse(Seconds2.Text) - 1).ToString();
            else Seconds2.Text = 0.ToString();
        }

        private void Seconds2ButtonUp_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if ((int.Parse(Seconds2.Text) + 1) < 9)
                Seconds2.Text = (int.Parse(Seconds2.Text) + 1).ToString();
            else Seconds2.Text = 9.ToString();
        }

        private void Seconds1ButtonUp_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if ((int.Parse(Seconds1.Text) + 1) < 6)
                Seconds1.Text = (int.Parse(Seconds1.Text) + 1).ToString();
            else Seconds1.Text = 5.ToString();
        }

        private void Seconds1ButtonDown_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if ((int.Parse(Seconds1.Text) - 1) > 0)
                Seconds1.Text = (int.Parse(Seconds1.Text) - 1).ToString();
            else Seconds1.Text = 0.ToString();
        }

        private void Minutes2ButtonDown_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if ((int.Parse(Minutes2.Text) - 1) > 0)
                Minutes2.Text = (int.Parse(Minutes2.Text) - 1).ToString();
            else Minutes2.Text = 0.ToString();
        }

        private void Minutes2ButtonUp_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if ((int.Parse(Minutes2.Text) + 1) < 9)
                Minutes2.Text = (int.Parse(Minutes2.Text) + 1).ToString();
            else Minutes2.Text = 9.ToString();
        }

        private void Minutes1ButtonDown_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if ((int.Parse(Minutes1.Text) - 1) > 0)
                Minutes1.Text = (int.Parse(Minutes1.Text) - 1).ToString();
            else Minutes1.Text = 0.ToString();
        }

        private void Minutes1ButtonUp_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if ((int.Parse(Minutes1.Text) + 1) < 6)
                Minutes1.Text = (int.Parse(Minutes1.Text) + 1).ToString();
            else Minutes1.Text = 5.ToString();
        }

        private void Hours2ButtonDown_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if ((int.Parse(Hours2.Text) - 1) > 0)
                Hours2.Text = (int.Parse(Hours2.Text) - 1).ToString();
            else Hours2.Text = 0.ToString();
        }

        private void Hours2ButtonUp_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if ((int.Parse(Hours2.Text) + 1) < 9)
                Hours2.Text = (int.Parse(Hours2.Text) + 1).ToString();
            else Hours2.Text = 9.ToString();
        }

        private void Hours1ButtonDown_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if ((int.Parse(Hours1.Text) - 1) > 0)
                Hours1.Text = (int.Parse(Hours1.Text) - 1).ToString();
            else Hours1.Text = 0.ToString();
        }

        private void Hours1ButtonUp_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if ((int.Parse(Hours1.Text) + 1) < 9)
                Hours1.Text = (int.Parse(Hours1.Text) + 1).ToString();
            else Hours1.Text = 9.ToString();
        }
    }
}
