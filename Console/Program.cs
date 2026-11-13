using Logic;
using Ninject;
using Presenter;
using System;
using System.Windows.Forms;
using WinForms;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleApp
{
    /// <summary>
    /// Точка входа в приложение (Сборка)
    /// </summary>
    class Program
    {
        /// <summary>
        /// Точка сборки. Связывает View, Presenter и Model.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 1. Создаем IoC-контейнер и достаем бизнес-логику
            IKernel kernel = new StandardKernel(new SimpleConfigModule());
            ILogic logic = kernel.Get<ILogic>();

            // 2. Создаем View
            MainForm view = new MainForm();

            // 3. Создаем Presenter (связываем View и Model)
            PhonePresenter presenter = new PhonePresenter(view, logic);

            // 4. Запускаем приложение
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(view);
        }
    }
}