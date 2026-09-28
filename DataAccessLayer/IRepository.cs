using System.Collections.Generic;
using Model;

namespace DataAccessLayer
{
    /// <summary>
    /// Интерфейс репозитория для CRUD-операций
    /// </summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    public interface IRepository<T> where T : IDomainObject
    {
        /// <summary>
        /// Добавить сущность
        /// </summary>
        /// <param name="entity">Сущность для добавления</param>
        void Add(T entity);
        /// <summary>
        /// Удалить сущность
        /// </summary>
        /// <param name="entity">Сущность для удаления</param>
        void Delete(T entity);
        /// <summary>
        /// Получить все сущности
        /// </summary>
        /// <returns>Список всех сущностей</returns>
        List<T> ReadAll();
        /// <summary>
        /// Получить сущность по ID
        /// </summary>
        /// <param name="id">ID сущности</param>
        /// <returns>Найденная сущность</returns>
        T ReadById(int id);
        /// <summary>
        /// Обновить сущность
        /// </summary>
        /// <param name="entity">Сущность для обновления</param>
        void Update(T entity);
    }
}