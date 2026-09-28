using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms
{
    /// <summary>
    /// Точка входа для приложения WinForms
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
