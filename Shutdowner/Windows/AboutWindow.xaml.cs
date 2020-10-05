using MahApps.Metro.Controls;
using System.Windows;

namespace Shutdowner
{
    /// <summary>
    /// Окно о приложении
    /// </summary>
    public partial class AboutWindow : MetroWindow
    {
        /// <summary>
        /// Создание окна
        /// </summary>
        /// <param name="owner">Родительское окно</param>
        public AboutWindow(Window owner)
        {
            InitializeComponent();
            Owner = owner;
        }
    }
}
