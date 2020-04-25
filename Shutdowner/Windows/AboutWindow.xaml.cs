using MahApps.Metro.Controls;
using System.Windows;

namespace Shutdowner
{
    /// <summary>
    /// Логика взаимодействия для AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : MetroWindow
    {
        public AboutWindow(Window owner)
        {
            InitializeComponent();
            Owner = owner;
        }
    }
}
