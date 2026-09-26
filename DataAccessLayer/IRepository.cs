using System.Collections.Generic;
using Model;

namespace DataAccessLayer
{
    public interface IRepository<T> where T : IDomainObject
    {
        void Add(T entity);
        void Delete(T entity);
        List<T> ReadAll();
        T ReadById(int id);
        void Update(T entity);
    }
}