using System;
using System.Windows.Forms;
using Logic;
using Ninject;
using WinForms;

namespace Presenter
{
    /// <summary>
    /// Общая точка сборки приложения
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Точка входа. Спрашивает пользователя, какое представление использовать.
        /// </summary>
        [STAThread]
        static void Main()
        {
            IKernel kernel = new StandardKernel(new SimpleConfigModule());
            ILogic logic = kernel.Get<ILogic>();

            Console.WriteLine("Выберите представление:");
            Console.WriteLine("1 — Консоль");
            Console.WriteLine("2 — WinForms");
            Console.Write("Ваш выбор: ");
            var choice = Console.ReadLine();

            if (choice == "1")
            {
                var consolePresenter = new ConsolePresenter(logic);
                consolePresenter.Run();
            }
            else if (choice == "2")
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                MainForm view = new MainForm();
                PhonePresenter presenter = new PhonePresenter(view, logic);
                Application.Run(view);
            }
            else
            {
                Console.WriteLine("Неверный выбор. Завершение работы.");
            }
        }
    }
}