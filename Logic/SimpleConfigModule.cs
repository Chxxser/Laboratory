using Ninject.Modules;
using DataAccessLayer;
using Model;

namespace Logic
{
    /// <summary>
    /// Модуль конфигурации зависимостей для Ninject
    /// </summary>
    public class SimpleConfigModule : NinjectModule
    {
        /// <summary>
        /// Загрузка зависимостей
        /// </summary>
        public override void Load()
        {
            Bind<IRepository<Phone>>()
                .ToMethod(context => new DapperRepository<Phone>("Phones"))
                .InSingletonScope();

            Bind<IPhoneService>().To<PhoneService>().InSingletonScope();
            Bind<IAnalyticsService>().To<AnalyticsService>().InSingletonScope();
            Bind<DataSeeder>().To<DataSeeder>().InSingletonScope();
            Bind<ILogic>().To<Logic>().InSingletonScope();
        }
    }
}