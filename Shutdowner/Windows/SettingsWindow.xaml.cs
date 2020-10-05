using MahApps.Metro.Controls;
using System.Windows;

namespace Shutdowner.Windows
{
    /// <summary>
    /// Окно настроек
    /// </summary>
    public partial class SettingsWindow : MetroWindow
    {
        /// <summary>
        /// Конструктор окна
        /// </summary>
        /// <param name="owner">Родительское окно</param>
        public SettingsWindow(Window owner)
        {
            InitializeComponent();
            Owner = owner;
        }
    }
}
