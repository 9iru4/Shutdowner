using MahApps.Metro.Controls;
using System.Windows;

namespace Shutdowner
{
    /// <summary>
    /// Логика взаимодействия для DonateWindow.xaml
    /// </summary>
    public partial class DonateWindow : MetroWindow
    {
        public DonateWindow(Window owner)
        {
            InitializeComponent();
            Owner = owner;
        }
    }
}
