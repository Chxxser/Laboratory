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
    public class DapperRepository : IRepository<Phone> 
    {
        public string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=PhoneDB;Integrated Security=True";
        /// <summary>
        /// Добавить сущность в базу данных
        /// </summary>
        /// <param name="entity">Сущность для добавления</param>
        public void Add(Phone entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = "INSERT INTO Phones (Brand, Model, Year, Color, Memory, Price, Availability) " +
                             "VALUES (@Brand, @Model, @Year, @Color, @Memory, @Price, @Availability)";
                connection.Execute(sql, entity);
            }
        }
        /// <summary>
        /// Удалить сущность из базы данных
        /// </summary>
        /// <param name="entity">Сущность для удаления</param>
        public void Delete(Phone entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = "DELETE FROM Phones WHERE Id = @Id";
                connection.Execute(sql, new { Id = entity.Id });
            }
        }
        /// <summary>
        /// Получить все сущности из базы данных
        /// </summary>
        /// <returns>Список всех сущностей</returns>
        public List<Phone> ReadAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Phones";
                return connection.Query<Phone>(sql).ToList();
            }
        }
        /// <summary>
        /// Получить сущность по ID
        /// </summary>
        /// <param name="id">ID сущности</param>
        /// <returns>Найденная сущность</returns>
        public Phone ReadById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Phones WHERE Id = @Id";
                return connection.QueryFirstOrDefault<Phone>(sql, new { Id = id });
            }
        }
        /// <summary>
        /// Обновить сущность в базе данных
        /// </summary>
        /// <param name="entity">Сущность для обновления</param>
        public void Update(Phone entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = "UPDATE Phones SET Brand = @Brand, Model = @Model, Year = @Year, " +
                             "Color = @Color, Memory = @Memory, Price = @Price, Availability = @Availability " +
                             "WHERE Id = @Id";
                connection.Execute(sql, entity);
            }
        }
    }
}