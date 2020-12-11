using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MineSweeper.Lib.PTL
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(List<String> navigationPropertyPaths);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate, List<String> navigationPropertyPaths);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
