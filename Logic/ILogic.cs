using Model;
using System.Collections.Generic;

namespace Logic
{
    /// <summary>
    /// Интерфейс бизнес-логики (Фасад)
    /// </summary>
    public interface ILogic
    {
        /// <summary>
        /// Добавить новый телефон
        /// </summary>
        /// <param name="brand">Бренд телефона</param>
        /// <param name="model">Модель телефона</param>
        /// <param name="year">Год выпуска</param>
        /// <param name="color">Цвет корпуса</param>
        /// <param name="price">Цена в рублях</param>
        /// <param name="memory">Память в ГБ</param>
        /// <param name="availability">Наличие на складе</param>
        /// <returns></returns>
        Phone AddPhone(string brand, string model, int year, string color, decimal price, int memory, bool availability);
        
        /// <summary>
        /// Получить список всех телефонов
        /// </summary>
        /// <returns>Список всех телефонов в БД</returns>
        List<Phone> AllPhone();

        /// <summary>
        /// Найти тлефон по ID
        /// </summary>
        /// <param name="id">ID телефона</param>
        /// <returns>Найденный телефон</returns>
        Phone PhoneId(int id);

        /// <summary>
        /// Обновить данные телефона
        /// </summary>
        /// <param name="id">ID телефона который нужен</param>
        /// <param name="brand">Бренд телефона</param>
        /// <param name="model">Модель теелфона</param>
        /// <param name="year">Год телефона</param>
        /// <param name="color">Цвет телефона</param>
        /// <param name="price">Цена телефона</param>
        /// <param name="memory">Память телефона</param>
        /// <param name="availability">В наличии ли</param>
        /// <returns>True если успешно, False при ошибке</returns>
        bool UpdatePhone(int id, string brand, string model, int year, string color, decimal price, int memory, bool availability);

        /// <summary>
        /// Удалить телефон
        /// </summary>
        /// <param name="id">ID телефона</param>
        /// <returns>True либо False при удалении</returns>
        bool DeletePhone(int id);

        /// <summary>
        /// Группировавка по памяти 
        /// </summary>
        /// <returns>Ключ - объем, значение - список</returns>
        Dictionary<int, List<Phone>> GroupMemory();

        /// <summary>
        /// Топ N дорогих тиелефнов
        /// </summary>
        /// <param name="count">Количество</param>
        /// <returns>Список N телефонов</returns>
        List<Phone> ExpensivePhones(int count);

        /// <summary>
        /// Вывести текстовые данныые
        /// </summary>
        void TestPhone();
    }
}