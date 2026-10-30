using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using Dapper;
using Model;

namespace DataAccessLayer
{
    /// <summary>
    /// Репозиторий для работы с базой данных через Dapper
    /// </summary>
    public class DapperRepository<T> : IRepository<T> where T : class, IDomainObject
    {
        public string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=PhoneDB;Integrated Security=True";

        private string _tableName;

        /// <summary>
        /// Конструктор репозитория с указанием имени таблицы
        /// </summary>
        public DapperRepository(string tableName)
        {
            _tableName = tableName;
        }

        /// <summary>
        /// Добавить сущность в базу данных
        /// </summary>
        public void Add(T entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                // Используем _tableName
                string sql = $@"
            INSERT INTO {_tableName} (Brand, Model, Year, Color, Memory, Price, Availability) 
            VALUES (@Brand, @Model, @Year, @Color, @Memory, @Price, @Availability);
            SELECT CAST(SCOPE_IDENTITY() as int);";

                var newId = connection.ExecuteScalar<int>(sql, entity);
                entity.Id = newId;
            }
        }

        /// <summary>
        /// Удалить сущность из базы данных
        /// </summary>
        public void Delete(T entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = $"DELETE FROM {_tableName} WHERE Id = @Id";
                connection.Execute(sql, new { Id = entity.Id });
            }
        }

        /// <summary>
        /// Получить все сущности из базы данных
        /// </summary>
        public List<T> ReadAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = $"SELECT * FROM {_tableName}";
                return connection.Query<T>(sql).ToList();
            }
        }

        /// <summary>
        /// Получить сущность по ID
        /// </summary>
        public T ReadById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = $"SELECT * FROM {_tableName} WHERE Id = @Id";
                return connection.QueryFirstOrDefault<T>(sql, new { Id = id });
            }
        }

        /// <summary>
        /// Обновить сущность в базе данных
        /// </summary>
        public void Update(T entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = $"UPDATE {_tableName} SET Brand = @Brand, Model = @Model, Year = @Year, " +
                             $"Color = @Color, Memory = @Memory, Price = @Price, Availability = @Availability " +
                             $"WHERE Id = @Id";
                connection.Execute(sql, entity);
            }
        }
    }
}