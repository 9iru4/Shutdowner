using MahApps.Metro.Controls;
using System.Windows;

namespace Shutdowner
{
    /// <summary>
    /// Окно доната
    /// </summary>
    public partial class DonateWindow : MetroWindow
    {
        /// <summary>
        /// Конструктор окно
        /// </summary>
        /// <param name="owner">Родительское окно</param>
        public DonateWindow(Window owner)
        {
            InitializeComponent();
            Owner = owner;
        }
    }
}
