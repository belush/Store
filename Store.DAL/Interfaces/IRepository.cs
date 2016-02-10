using System;
using System.Collections.Generic;

namespace Store.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        IEnumerable<T> Find(Func<T, bool> predicate);
        void Add(T entity);
        void Delete(int id);
        void Edit(T entity);
    }
}