using Model;
using System.Collections.Generic;

namespace Logic
{
    /// <summary>
    /// Интерфейс для бизнес-аналитики (не связанной с изменением данных)
    /// </summary>
    public interface IAnalyticsService
    {
        /// <summary>
        /// Сгруппировать телефоны по объему памяти
        /// </summary>
        /// <returns>Словарь: ключ - память (ГБ), значение - список телефонов</returns>
        Dictionary<int, List<Phone>> GroupMemory();

        /// <summary>
        /// Получить Топ-N самых дорогих телефонов
        /// </summary>
        /// <param name="count">Количество телефонов для топа</param>
        /// <returns>Список самых дорогих телефонов</returns>
        List<Phone> ExpensivePhones(int count);
    }
}