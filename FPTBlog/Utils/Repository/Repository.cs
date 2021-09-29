using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FPTBlog.Utils.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace FPTBlog.Utils.Repository
{
    public class Repository<T> : IRepository<T> where T : class {

        private readonly DB _db;
        internal DbSet<T> DbSet;

        public void Add(T entity) {
            DbSet.Add(entity);
        }

        public T Get(string key) {
            return DbSet.Find(key);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IEnumerable<T>> orderBy = null, string includeProperties = null) {
            IQueryable<T> query = DbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }

            return query.ToList();
        }

        public T GetByField(Expression<Func<T, bool>> filter = null, string includeProperties = null) {
            IQueryable<T> query = DbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return query.FirstOrDefault();
        }

        public void Remove(string key) {
            var entity = DbSet.Find(key);
            DbSet.Remove(entity);
        }

        public void Remove(T entity) {
            DbSet.Remove(entity);
        }

        public void Remove(IEnumerable<T> entities) {
            DbSet.RemoveRange(entities);
        }

        public void Update(T entity) {
            DbSet.Update(entity);
        }
    }
}
