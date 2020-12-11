using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MineSweeper.Lib.PTL
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationContext context;
        protected DbSet<T> entityDBSet;
        string errorMessage = string.Empty;

        public Repository(ApplicationContext context)
        {
            this.context = context;
            entityDBSet = context.Set<T>();
        }

        /*Get All without including child entities*/
        public IEnumerable<T> GetAll()
        {
            return entityDBSet.AsEnumerable();
        }

        /*Get All with including child entities*/
        public IEnumerable<T> GetAll(List<String> navigationPropertyPaths)
        {

            var query = entityDBSet.AsQueryable();

            foreach (var navigationPropertyPath in navigationPropertyPaths)
            {
                query = query.Include(navigationPropertyPath);
            }

            return query.AsEnumerable();
        }

        /*Find without including child entities*/
        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return entityDBSet.Where(predicate).ToList();
        }

        /*Find with including child entities*/
        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate, List<String> navigationPropertyPaths)
        {
            var query = entityDBSet.AsQueryable();

            foreach (var navigationPropertyPath in navigationPropertyPaths)
            {
                query = query.Include(navigationPropertyPath);
            }

            return query.Where(predicate).ToList();

        }

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entityDBSet.Add(entity);
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entityDBSet.Update(entity);
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entityDBSet.Remove(entity);
        }

    }
}
