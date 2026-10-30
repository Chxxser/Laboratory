using Model;
using System.Collections.Generic;

namespace Logic
{
    /// <summary>
    /// Фасад, объединяющий работу всех сервисов
    /// </summary>
    public class Logic : ILogic
    {
        public IPhoneService _phoneService;
        public IAnalyticsService _analyticsService;
        public DataSeeder _dataSeeder;

        /// <summary>
        /// Конструктор фасада
        /// </summary>
        /// <param name="phoneService">CRUD операции</param>
        /// <param name="analyticsService">Бизнес операции</param>
        /// <param name="dataSeeder">Вывод текста</param>
        public Logic(IPhoneService phoneService, IAnalyticsService analyticsService, DataSeeder dataSeeder)
        {
            _phoneService = phoneService;
            _analyticsService = analyticsService;
            _dataSeeder = dataSeeder;
        }

        /// <summary>
        /// Добавить новый телефон
        /// </summary>
        /// <param name="brand">Бренд телефона</param>
        /// <param name="model">Модель телефона</param>
        /// <param name="year">Год выпуска</param>
        /// <param name="color">Цвет корпуса</param>
        /// <param name="price">Цена в рублях</param>
        /// <param name="memory">Объем памяти в ГБ</param>
        /// <param name="availability">Наличие на складе</param>
        /// <returns>Созданный телефон</returns>
        public Phone AddPhone(string brand, string model, int year, string color, decimal price, int memory, bool availability)
            => _phoneService.AddPhone(brand, model, year, color, price, memory, availability);

        /// <summary>
        /// Список всех телефонов
        /// </summary>
        /// <returns>Список всех телефонов</returns>
        public List<Phone> AllPhone() => _phoneService.AllPhone();

        /// <summary>
        /// Найти телефон по ID
        /// </summary>
        /// <param name="id">ID телефона</param>
        /// <returns>Найденный телефон</returns>
        public Phone PhoneId(int id) => _phoneService.PhoneId(id);

        /// <summary>
        /// Обновить данные
        /// </summary>
        /// <param name="id">ID телефона</param>
        /// <param name="brand">Новый бренд</param>
        /// <param name="model">Новая модель</param>
        /// <param name="year">Новый год выпуска</param>
        /// <param name="color">Новый цвет</param>
        /// <param name="price">Новая цена</param>
        /// <param name="memory">Новый объем памяти</param>
        /// <param name="availability">В наличии ли</param>
        /// <returns>True если успешно, False при ошибке</returns>
        public bool UpdatePhone(int id, string brand, string model, int year, string color, decimal price, int memory, bool availability)
            => _phoneService.UpdatePhone(id, brand, model, year, color, price, memory, availability);

        /// <summary>
        /// Удалить телефон
        /// </summary>
        /// <param name="id">ID телефона</param>
        /// <returns>True если успешно, False при ошибке</returns>
        public bool DeletePhone(int id) => _phoneService.DeletePhone(id);

        /// <summary>
        /// Группировка по памяти
        /// </summary>
        /// <returns>Список где ключ - обхем а значение телефоны</returns>
        public Dictionary<int, List<Phone>> GroupMemory() => _analyticsService.GroupMemory();

        /// <summary>
        /// Топ N дорогих
        /// </summary>
        /// <param name="count">количество N</param>
        /// <returns>Список телефонов</returns>
        public List<Phone> ExpensivePhones(int count) => _analyticsService.ExpensivePhones(count);

        /// <summary>
        /// Заполнить текстом
        /// </summary>
        public void TestPhone() => _dataSeeder.Seed();
    }
}