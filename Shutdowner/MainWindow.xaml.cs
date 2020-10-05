using MahApps.Metro.Controls;
using Shutdowner.Windows;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Shutdowner
{
    public enum TaskTypes { Выключение = 0, Перезагрузка, Гибернация, Приложение }

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        /// <summary>
        /// Настройки для запуска приложения
        /// </summary>
        (bool, string, string) AppSettings = (false, "", "");
        /// <summary>
        /// Таймер
        /// </summary>
        MyCountDownTimer timer = new MyCountDownTimer();

        /// <summary>
        /// Планировщик
        /// </summary>
        MySheduler mySheduler = new MySheduler();

        public MainWindow()
        {
            InitializeComponent();

            TaskTypeButton.ItemsSource = Enum.GetValues(typeof(TaskTypes)).Cast<TaskTypes>();

            //Событие изменения выбранного задания
            TaskTypeButton.SelectionChanged += TaskTypeButton_SelectionChanged;

            //Событие тика таймера
            timer.TimerTick += Timer_TimeChangedEvent;
            //Событие остановки таймера
            timer.TimerStop += Timer_TimerStop;
            //Событие изменения вкладки
            AppTabs.SelectionChanged += AppTabs_SelectionChanged;
            //Загрузка задач из планировщика
            TasksDataGrid.ItemsSource = mySheduler.MyTasks;
        }

        /// <summary>
        /// Метод смены активной вкладки
        /// </summary>
        private void AppTabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((AppTabs.SelectedItem as TabItem).Name == "CreateTaskTab")
                TaskTypeButton_SelectionChanged(null, null);//если вкладка создать задачу, проверяем активные таймеры
            if ((AppTabs.SelectedItem as TabItem).Name == "TaskHistoryTab")
            {
                mySheduler.LoadTasks();
            }
        }

        /// <summary>
        /// Событие смены типа задачи
        /// </summary>
        private void TaskTypeButton_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch ((TaskTypes)TaskTypeButton.SelectedValue)
            {
                case TaskTypes.Выключение://выключение
                    {
                        if (timer.IsTimerStarted()) StopTimer();
                        foreach (var task in mySheduler.MyTasks)
                        {
                            //если есть задача, создаем таймер для нее
                            if (task.Description == "Запланированное выключение" && task.Enabled)
                            {
                                SetTime(MyCountDownTimer.GetTime((int)(task.DateTrigger - DateTime.Now).TotalSeconds));
                                StartTimer();
                            }
                        }
                    }
                    break;
                case TaskTypes.Перезагрузка://перезагрузка
                    {
                        if (timer.IsTimerStarted()) StopTimer();
                        foreach (var task in mySheduler.MyTasks)
                        {
                            //если есть задача, создаем таймер для нее
                            if (task.Description == "Запланированная перезагрузка" && task.Enabled)
                            {
                                SetTime(MyCountDownTimer.GetTime((int)(task.DateTrigger - DateTime.Now).TotalSeconds));
                                StartTimer();
                            }
                        }
                    }
                    break;
                case TaskTypes.Гибернация://гибернация
                    {
                        if (timer.IsTimerStarted()) StopTimer();
                        foreach (var task in mySheduler.MyTasks)
                        {
                            //если есть задача, создаем таймер для нее
                            if (task.Description == "Запланированная гибернация" && task.Enabled)
                            {
                                SetTime(MyCountDownTimer.GetTime((int)(task.DateTrigger - DateTime.Now).TotalSeconds));
                                StartTimer();
                            }
                        }
                    }
                    break;
                case TaskTypes.Приложение://сброс таймера, так как нет смысла его отбражать
                    if (timer.IsTimerStarted()) StopTimer();
                    AppSettings = (false, "", "");
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Остановка таймера
        /// </summary>
        private void Timer_TimerStop()
        {
            StopTimer();
        }

        /// <summary>
        /// Тик таймера
        /// </summary>
        private void Timer_TimeChangedEvent()
        {
            SetTime(timer.GetTime());
        }

        /// <summary>
        /// Установка времени на таймере
        /// </summary>
        /// <param name="time">Время в формате Ч Ч М М С С</param>
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

        /// <summary>
        /// Вызов окна о программе
        /// </summary>
        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow(this);
            aboutWindow.ShowDialog();
        }

        /// <summary>
        /// Вызов окна настройки
        /// </summary>
        private void SettingButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow(this);
            settingsWindow.ShowDialog();
        }

        /// <summary>
        /// Вызов окна поддержать разработчика
        /// </summary>
        private void DonateButton_Click(object sender, RoutedEventArgs e)
        {
            DonateWindow donateWindow = new DonateWindow(this);
            donateWindow.ShowDialog();
        }

        /// <summary>
        /// Скрытие кнопок таймера
        /// </summary>
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
        }

        /// <summary>
        /// Отображение кнопок таймера
        /// </summary>
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
        }

        /// <summary>
        /// Получение количества секунд установленных на таймере
        /// </summary>
        /// <returns>Количество секунд</returns>
        int GetSeconds()
        {
            return int.Parse(Hours1.Text + Hours2.Text) * 60 * 60 + int.Parse(Minutes1.Text + Minutes2.Text) * 60 + int.Parse(Seconds1.Text + Seconds2.Text);
        }

        /// <summary>
        /// Запуск таймера
        /// </summary>
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (GetSeconds() > 0)
            {
                if (StartButton.Content.ToString() == "пуск")
                {
                    switch ((TaskTypes)TaskTypeButton.SelectedValue)
                    {
                        case TaskTypes.Выключение:
                            mySheduler.CreateNewTaskAtTime("Запланированное выключение", "shutdown.exe", "/s", DateTime.Now.AddSeconds(GetSeconds() + 1));
                            StartTimer();
                            break;
                        case TaskTypes.Перезагрузка:
                            mySheduler.CreateNewTaskAtTime("Запланированная перезагрузка", "shutdown.exe", "/r", DateTime.Now.AddSeconds(GetSeconds() + 1));
                            StartTimer();
                            break;
                        case TaskTypes.Гибернация:
                            mySheduler.CreateNewTaskAtTime("Запланированная гибернация", "shutdown.exe", "/h", DateTime.Now.AddSeconds(GetSeconds() + 1));
                            StartTimer();
                            break;
                        case TaskTypes.Приложение:
                            {
                                if (AppSettings.Item2 != "")
                                {
                                    if (AppSettings.Item1)
                                    {
                                        mySheduler.CreateNewTaskAtTime("Приложение", AppSettings.Item2, AppSettings.Item3, DateTime.Now.AddSeconds(GetSeconds() + 1));
                                    }
                                    else
                                    {
                                        mySheduler.CreateNewTaskAtTime("Приложение", "taskkill", "/f /im " + AppSettings.Item2.Split('\\').LastOrDefault(), DateTime.Now.AddSeconds(GetSeconds() + 1));
                                    }
                                    StartTimer();
                                }
                                else
                                {
                                    MessageWindow mw = new MessageWindow(this, "Уведомление", "Установите настройки для приложения, для этого нажмите на кнопку \"Приложение\".");
                                    mw.ShowDialog();
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    StopTimer();
                }
            }
            else
            {
                MessageWindow mw = new MessageWindow(this, "Уведомление", "Для запуска таймера, установите время.");
                mw.ShowDialog();
            }
        }

        /// <summary>
        /// Запуск таймера
        /// </summary>
        void StartTimer()
        {
            StartButton.Content = "сброс";
            HideUpDownButtons();
            StartButton.Background = new SolidColorBrush(Colors.Red);
            StartButton.Foreground = new SolidColorBrush(Colors.White);
            timer.StartTimer(GetSeconds());
        }

        /// <summary>
        /// Остановка таймера
        /// </summary>
        /// <param name="reset">Сбросить ли текущий таймер</param>
        void StopTimer()
        {
            timer.StopTimer();
            ResetTimer();
            ShowUpDownButons();
            StartButton.Content = "пуск";
            StartButton.Background = new SolidColorBrush(Color.FromRgb(65, 177, 225));
        }

        /// <summary>
        /// Установка таймера в 0
        /// </summary>
        void ResetTimer()
        {
            Seconds2.Text = "0";
            Seconds1.Text = "0";
            Minutes2.Text = "0";
            Minutes1.Text = "0";
            Hours2.Text = "0";
            Hours1.Text = "0";
        }

        /// <summary>
        /// Уменьшение цифры секунд 2
        /// </summary>
        private void Seconds2ButtonDown_Click(object sender, RoutedEventArgs e)
        {
            if ((int.Parse(Seconds2.Text) - 1) > 0)
                Seconds2.Text = (int.Parse(Seconds2.Text) - 1).ToString();
            else Seconds2.Text = 0.ToString();
        }

        /// <summary>
        /// Увеличение цифры секунд 2
        /// </summary>
        private void Seconds2ButtonUp_Click(object sender, RoutedEventArgs e)
        {
            if ((int.Parse(Seconds2.Text) + 1) < 9)
                Seconds2.Text = (int.Parse(Seconds2.Text) + 1).ToString();
            else Seconds2.Text = 9.ToString();
        }

        /// <summary>
        /// Увеличение цифры секунд 1
        /// </summary>
        private void Seconds1ButtonUp_Click(object sender, RoutedEventArgs e)
        {
            if ((int.Parse(Seconds1.Text) + 1) < 6)
                Seconds1.Text = (int.Parse(Seconds1.Text) + 1).ToString();
            else Seconds1.Text = 5.ToString();
        }

        /// <summary>
        /// Уменьшение цифры секунд 1
        /// </summary>
        private void Seconds1ButtonDown_Click(object sender, RoutedEventArgs e)
        {
            if ((int.Parse(Seconds1.Text) - 1) > 0)
                Seconds1.Text = (int.Parse(Seconds1.Text) - 1).ToString();
            else Seconds1.Text = 0.ToString();
        }

        /// <summary>
        /// Уменьшение цифры минут 2
        /// </summary>
        private void Minutes2ButtonDown_Click(object sender, RoutedEventArgs e)
        {
            if ((int.Parse(Minutes2.Text) - 1) > 0)
                Minutes2.Text = (int.Parse(Minutes2.Text) - 1).ToString();
            else Minutes2.Text = 0.ToString();
        }

        /// <summary>
        /// Увеличение цифры минут 2
        /// </summary>
        private void Minutes2ButtonUp_Click(object sender, RoutedEventArgs e)
        {
            if ((int.Parse(Minutes2.Text) + 1) < 9)
                Minutes2.Text = (int.Parse(Minutes2.Text) + 1).ToString();
            else Minutes2.Text = 9.ToString();
        }

        /// <summary>
        /// Уменьшение цифры минут 1
        /// </summary>
        private void Minutes1ButtonDown_Click(object sender, RoutedEventArgs e)
        {
            if ((int.Parse(Minutes1.Text) - 1) > 0)
                Minutes1.Text = (int.Parse(Minutes1.Text) - 1).ToString();
            else Minutes1.Text = 0.ToString();
        }

        /// <summary>
        /// Увеличение цифры минут 1
        /// </summary>
        private void Minutes1ButtonUp_Click(object sender, RoutedEventArgs e)
        {
            if ((int.Parse(Minutes1.Text) + 1) < 6)
                Minutes1.Text = (int.Parse(Minutes1.Text) + 1).ToString();
            else Minutes1.Text = 5.ToString();
        }

        /// <summary>
        /// Уменьшение цифры часов 2
        /// </summary>
        private void Hours2ButtonDown_Click(object sender, RoutedEventArgs e)
        {
            if ((int.Parse(Hours2.Text) - 1) > 0)
                Hours2.Text = (int.Parse(Hours2.Text) - 1).ToString();
            else Hours2.Text = 0.ToString();
        }

        /// <summary>
        /// Увеличение цифры часов 2
        /// </summary>
        private void Hours2ButtonUp_Click(object sender, RoutedEventArgs e)
        {
            if ((int.Parse(Hours2.Text) + 1) < 9)
                Hours2.Text = (int.Parse(Hours2.Text) + 1).ToString();
            else Hours2.Text = 9.ToString();
        }

        /// <summary>
        /// Уменьшение цифры часов 1
        /// </summary>
        private void Hours1ButtonDown_Click(object sender, RoutedEventArgs e)
        {
            if ((int.Parse(Hours1.Text) - 1) > 0)
                Hours1.Text = (int.Parse(Hours1.Text) - 1).ToString();
            else Hours1.Text = 0.ToString();
        }

        /// <summary>
        /// Увеличение цифры часов 1
        /// </summary>
        private void Hours1ButtonUp_Click(object sender, RoutedEventArgs e)
        {
            if ((int.Parse(Hours1.Text) + 1) < 9)
                Hours1.Text = (int.Parse(Hours1.Text) + 1).ToString();
            else Hours1.Text = 9.ToString();
        }

        /// <summary>
        /// Отменить задание
        /// </summary>
        private void CancelTaskButton_Click(object sender, RoutedEventArgs e)
        {
            mySheduler.DisableTaskStatus(TasksDataGrid.SelectedItem as MyTaskView);
        }

        /// <summary>
        /// Удалить все задания
        /// </summary>
        private void DeleteAllTaskHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var task in mySheduler.MyTasks.ToList())
            {
                mySheduler.RemoveTask(task);
            }
        }

        /// <summary>
        /// Нажатие на кнопку типа задания
        /// </summary>
        private void TaskTypeButton_Click(object sender, RoutedEventArgs e)
        {
            switch ((TaskTypes)(sender as SplitButton).SelectedValue)
            {
                case TaskTypes.Приложение:
                    {
                        ApplicationWindow ap = new ApplicationWindow(this);
                        ap.ShowDialog();
                        AppSettings.Item1 = (bool)ap.AppTaskTypeSwitch.IsChecked;
                        AppSettings.Item2 = ap.PathToAppTextBox.Text;
                        AppSettings.Item3 = ap.ArgumentsTextBox.Text;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
