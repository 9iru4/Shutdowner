using MahApps.Metro.Controls;
using System.Windows;

namespace Shutdowner.Windows
{
    /// <summary>
    /// Окно сообщения
    /// </summary>
    public partial class MessageWindow : MetroWindow
    {
        /// <summary>
        /// Конструктор окна
        /// </summary>
        /// <param name="owner">Родительское окно</param>
        /// <param name="windowName">Название окна</param>
        /// <param name="windowMessage">Сообщение</param>
        public MessageWindow(Window owner, string windowName, string windowMessage)
        {
            InitializeComponent();
            Owner = owner;
            Title = windowName;
            MessageText.Content = windowMessage;
        }

        /// <summary>
        /// Нажатие кнопки ок
        /// </summary>
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
