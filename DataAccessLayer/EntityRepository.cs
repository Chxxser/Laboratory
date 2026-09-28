using System.Collections.Generic;
using System.Linq;
using Model;

namespace DataAccessLayer
{
    /// <summary>
    /// Репозиторий для работы с базой данных через EF
    /// </summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    public class EntityRepository<T> : IRepository<T> where T : class, IDomainObject
    {
        public DBContext _context;
        /// <summary>
        /// Конструктор репозитория
        /// </summary>
        public EntityRepository()
        {
            _context = new DBContext();

        }
        /// <summary>
        /// Добавить сущность в базу данных
        /// </summary>
        /// <param name="entity">Сущность для добавления</param>
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }
        /// <summary>
        /// Удалить сущность из базы данных
        /// </summary>
        /// <param name="entity">Сущность для удаления</param>
        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }
        /// <summary>
        /// Получить все сущности из базы данных
        /// </summary>
        /// <returns>Список всех сущностей</returns>
        public List<T> ReadAll()
        {
            return _context.Set<T>().ToList();
        }
        /// <summary>
        /// Получить сущность по ID
        /// </summary>
        /// <param name="id">ID сущности</param>
        /// <returns>Найденная сущность</returns>
        public T ReadById(int id)
        {
            return _context.Set<T>().FirstOrDefault(e => e.Id == id);
        }
        /// <summary>
        /// Обновить сущность в базе данных
        /// </summary>
        /// <param name="entity">Сущность для обновления</param>
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }
    }
}