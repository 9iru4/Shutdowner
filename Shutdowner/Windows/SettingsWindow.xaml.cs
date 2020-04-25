using MahApps.Metro.Controls;
using System.Windows;

namespace Shutdowner.Windows
{
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : MetroWindow
    {
        public SettingsWindow(Window owner)
        {
            InitializeComponent();
            Owner = owner;
        }
    }
}
