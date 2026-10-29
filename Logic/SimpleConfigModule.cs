using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            // Связываем интерфейс IRepository<Phone> с классом EntityRepository<Phone> либо DapperRepository
            Bind<IRepository<Phone>>().To<EntityRepository<Phone>>().InSingletonScope();
            // Bind<IRepository<Phone>>().To<DapperRepository>();
        }
    }
}