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
        [STAThread]
        static void Main()
        {
            // 1. Создаем IoC-контейнер и достаем бизнес-логику
            IKernel kernel = new StandardKernel(new SimpleConfigModule());
            ILogic logic = kernel.Get<ILogic>();

            // 2. Спрашиваем у пользователя, какое представление использовать
            Console.WriteLine("Выберите представление:");
            Console.WriteLine("1 — Консоль");
            Console.WriteLine("2 — WinForms");
            Console.Write("Ваш выбор: ");
            var choice = Console.ReadLine();

            if (choice == "1")
            {
                // Запуск консольного представления
                // ВАЖНО: Чтобы это работало, класс ConsolePresenter должен быть в этом же проекте (или в Console)
                // Если у тебя его нет, используй старый код из Console, но вызови его здесь.
                var consolePresenter = new ConsolePresenter(logic);
                consolePresenter.Run();
            }
            else if (choice == "2")
            {
                // Запуск WinForms
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