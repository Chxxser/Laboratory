using Model;
using System.Collections.Generic;

namespace Logic
{
    /// <summary>
    /// Реализация бизнес-аналитики для телефонов
    /// </summary>
    public class AnalyticsService : IAnalyticsService
    {
        public IPhoneService _phoneService;

        /// <summary>
        /// Конструктор сервиса аналитики
        /// </summary>
        /// <param name="phoneService">Сервис для получения списка телефонов</param>
        public AnalyticsService(IPhoneService phoneService)
        {
            _phoneService = phoneService;
        }

        /// <summary>
        /// Сгруппировать телефоны по объему памяти
        /// </summary>
        /// <returns>Словарь где ключ - объем, значение - список с памятью этой</returns>
        public Dictionary<int, List<Phone>> GroupMemory()
        {
            Dictionary<int, List<Phone>> result = new Dictionary<int, List<Phone>>();
            foreach (Phone i in _phoneService.AllPhone())
            {
                if (result.ContainsKey(i.Memory) == false) { result[i.Memory] = new List<Phone>(); }
                result[i.Memory].Add(i);
            }
            return result;
        }

        /// <summary>
        /// Топ дорогих
        /// </summary>
        /// <param name="count">Количество N</param>
        /// <returns>Список N телефонов по убыванию</returns>
        public List<Phone> ExpensivePhones(int count)
        {
            List<Phone> sorted = new List<Phone>();
            foreach (Phone i in _phoneService.AllPhone()) { sorted.Add(i); }

            for (int i = 0; i < sorted.Count - 1; i++)
            {
                for (int j = 0; j < sorted.Count - i - 1; j++)
                {
                    if (sorted[j].Price < sorted[j + 1].Price)
                    {
                        Phone a = sorted[j];
                        sorted[j] = sorted[j + 1];
                        sorted[j + 1] = a;
                    }
                }
            }

            List<Phone> result = new List<Phone>();
            int limit;
            if (count < sorted.Count) { limit = count; }
            else { limit = sorted.Count; }
            for (int i = 0; i < limit; i++) { result.Add(sorted[i]); }
            return result;
        }
    }
}