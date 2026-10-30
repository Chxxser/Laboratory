using Model;
using System.Collections.Generic;

namespace Logic
{
    /// <summary>
    /// Интерфейс для CRUD-операций с телефонами
    /// </summary>
    public interface IPhoneService
    {
        /// <summary>
        /// Добавить новый телефон
        /// </summary>
        /// <param name="brand">Бренд телефона</param>
        /// <param name="model">Модель телефона</param>
        /// <param name="year">Год выпуска</param>
        /// <param name="color">Цвет корпуса</param>
        /// <param name="price">Цена в рублях</param>
        /// <param name="memory">Объем памяти в ГБ</param>
        /// <param name="availability">В наличии ли</param>
        /// <returns>Созданный телефон</returns>
        Phone AddPhone(string brand, string model, int year, string color, decimal price, int memory, bool availability);

        /// <summary>
        /// Получить список всех телефонов
        /// </summary>
        /// <returns>Список всех телефонов</returns>
        List<Phone> AllPhone();

        /// <summary>
        /// Найти телефон по ID
        /// </summary>
        /// <param name="id">ID телефона</param>
        Phone PhoneId(int id);

        /// <summary>
        /// Обновить данные телефона
        /// </summary>
        /// <param name="id">ID телефона</param>
        /// <param name="brand">Бренд телефона</param>
        /// <param name="model">Модель телефона</param>
        /// <param name="year">Год выпуска</param>
        /// <param name="color">Цвет корпуса</param>
        /// <param name="price">Цена в рублях</param>
        /// <param name="memory">Объем памяти в ГБ</param>
        /// <param name="availability">В наличии ли</param>
        /// <returns>True при обновлении, False при ошибке</returns>
        bool UpdatePhone(int id, string brand, string model, int year, string color, decimal price, int memory, bool availability);

        /// <summary>
        /// Удалить телефон
        /// </summary>
        /// <param name="id">ID телефона</param>
        /// <returns>True, если удаление прошло успешно</returns>
        bool DeletePhone(int id);
    }
}