using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Model;

namespace DataAccessLayer
{
    public class DapperRepository<T> : IRepository<T> where T : class, IDomainObject
    {
        public string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True";

        public void Add(T entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = "INSERT INTO Phones (Brand, Model, Year, Color, Memory, Price, Availability) " +
                             "VALUES (@Brand, @Model, @Year, @Color, @Memory, @Price, @Availability)";
                connection.Execute(sql, entity);
            }
        }

        public void Delete(T entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = "DELETE FROM Phones WHERE Id = @Id";
                connection.Execute(sql, new { Id = entity.Id });
            }
        }

        public List<T> ReadAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Phones";
                return connection.Query<T>(sql).ToList();
            }
        }

        public T ReadById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Phones WHERE Id = @Id";
                return connection.QueryFirstOrDefault<T>(sql, new { Id = id });
            }
        }

        public void Update(T entity)
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