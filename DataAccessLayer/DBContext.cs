using Microsoft.EntityFrameworkCore;
using Model;

namespace DataAccessLayer
{
    /// <summary>
    /// Контекст базы данных для EF
    /// </summary>
    public class DBContext : DbContext
    {
        public DbSet<Phone> Phones { get; set; }
        /// <summary>
        /// Конструктор контекста. Создаёт базу данных, если её нет
        /// </summary>
        public DBContext() { Database.EnsureCreated(); }
        /// <summary>
        /// Настройка подключения к базе данных
        /// </summary>
        /// <param name="optionsBuilder">Параметры подключения</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=PhoneDB;Integrated Security=True");
        }
    }
}