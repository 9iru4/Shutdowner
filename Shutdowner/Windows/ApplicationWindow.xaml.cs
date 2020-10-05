using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.Windows;

namespace Shutdowner.Windows
{
    /// <summary>
    /// Окно настроек для запуска задания
    /// </summary>
    public partial class ApplicationWindow : MetroWindow
    {
        /// <summary>
        /// Конструктор окна
        /// </summary>
        /// <param name="owner">Владелец окна</param>
        public ApplicationWindow(Window owner)
        {
            InitializeComponent();
            Owner = owner;
            ArgumentsLabel.Visibility = Visibility.Hidden;
            ArgumentsTextBox.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Метод выбора пути к приложению
        /// </summary>
        private void ChooseAppButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Executable files (*.exe)|*.exe|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                var FilePath = openFileDialog.FileName;
                PathToAppTextBox.Text = FilePath;
            }
        }

        /// <summary>
        /// Кнопка сохранить настройки
        /// </summary>
        private void SaveAppSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Событие изменения состояния переключателя
        /// </summary>
        private void AppTaskTypeSwitch_IsCheckedChanged(object sender, EventArgs e)
        {
            if ((bool)(sender as ToggleSwitch).IsChecked)
            {
                ArgumentsLabel.Visibility = Visibility.Visible;
                ArgumentsTextBox.Visibility = Visibility.Visible;
            }
            else
            {
                ArgumentsLabel.Visibility = Visibility.Hidden;
                ArgumentsTextBox.Visibility = Visibility.Hidden;
            }
        }
    }
}
