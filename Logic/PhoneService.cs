using DataAccessLayer;
using Model;
using System.Collections.Generic;

namespace Logic
{
    /// <summary>
    /// Реализация CRUD-операций для телефонов
    /// </summary>
    public class PhoneService : IPhoneService
    {
        public IRepository<Phone> _repository;

        /// <summary>
        /// Конструктор сервиса
        /// </summary>
        /// <param name="repository">Репозиторий для доступа к данным</param>
        public PhoneService(IRepository<Phone> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Добавить телефон
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
        {
            var phone = new Phone()
            {
                Brand = brand,
                Model = model,
                Year = year,
                Color = color,
                Price = price,
                Memory = memory,
                Availability = availability
            };
            _repository.Add(phone);
            return phone;
        }

        /// <summary>
        /// Поолучить список всех теелфгонов
        /// </summary>
        /// <returns>Список телеофнов</returns>
        public List<Phone> AllPhone()
        {
            return _repository.ReadAll();
        }

        /// <summary>
        /// Найти по ID
        /// </summary>
        /// <param name="id">ID телефона</param>
        /// <returns>найденный телефон</returns>
        public Phone PhoneId(int id)
        {
            return _repository.ReadById(id);
        }

        /// <summary>
        /// Обновить телефон
        /// </summary>
        /// <param name="id">ID телефонга</param>
        /// <param name="brand">Бренд телефона</param>
        /// <param name="model">Модель телефона</param>
        /// <param name="year">Год выпуска</param>
        /// <param name="color">Цвет корпуса</param>
        /// <param name="price">Цена в рублях</param>
        /// <param name="memory">Объем памяти в ГБ</param>
        /// <param name="availability">Наличие на складе</param>
        /// <returns>True есл и успешно</returns>
        public bool UpdatePhone(int id, string brand, string model, int year, string color, decimal price, int memory, bool availability)
        {
            Phone phone = _repository.ReadById(id);
            if (phone == null) return false;

            phone.Brand = brand;
            phone.Model = model;
            phone.Year = year;
            phone.Color = color;
            phone.Price = price;
            phone.Memory = memory;
            phone.Availability = availability;

            _repository.Update(phone);
            return true;
        }

        /// <summary>
        /// Удалить телефон
        /// </summary>
        /// <param name="id">ID телефона</param>
        /// <returns>True при удалении False при ошибке</returns>
        public bool DeletePhone(int id)
        {
            Phone phone = _repository.ReadById(id);
            if (phone == null) return false;
            _repository.Delete(phone);
            return true;
        }
    }
}