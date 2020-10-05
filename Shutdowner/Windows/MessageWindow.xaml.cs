using MahApps.Metro.Controls;
using System.Windows;

namespace Shutdowner.Windows
{
    /// <summary>
    /// Логика взаимодействия для MessageWindow.xaml
    /// </summary>
    public partial class MessageWindow : MetroWindow
    {
        public MessageWindow(Window owner, string windowName, string windowMessage)
        {
            InitializeComponent();
            Owner = owner;
            Title = windowName;
            MessageText.Content = windowMessage;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
