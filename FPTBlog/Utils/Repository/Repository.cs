using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FPTBlog.Utils.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace FPTBlog.Utils.Repository {
    public class Repository<T> : IRepository<T> where T : class {

        private readonly DB _db;
        internal DbSet<T> DbSet;

        public Repository(DB db) {
            _db = db;
            DbSet = _db.Set<T>();
        }

        public void Add(T entity) {
            DbSet.Add(entity);
            _db.SaveChanges();
        }

        public T Get(string key) {
            return DbSet.Find(key);
        }

        public ICollection<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, ICollection<T>> options = null, string includeProperties = null) {
            IQueryable<T> query = DbSet;
            if (filter != null) {
                query = query.Where(filter);
            }

            if (includeProperties != null) {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) {
                    query = query.Include(includeProp);
                }
            }

            if (options != null) {
                return options(query).ToList();
            }

            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null) {
            IQueryable<T> query = DbSet;
            if (filter != null) {
                query = query.Where(filter);
            }

            if (includeProperties != null) {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) {
                    query = query.Include(includeProp);
                }
            }

            return query.FirstOrDefault();
        }


        // public IEnumerable<T> GetEntityByPage(IEnumerable<T> enumrable, int pageSize, int pageIndex) =>
        //     enumrable.Take((pageIndex + 1) * pageSize).Skip(pageIndex * pageSize).ToList();

        public void Remove(string key) {
            var entity = DbSet.Find(key);
            DbSet.Remove(entity);
            _db.SaveChanges();
        }

        public void Remove(T entity) {
            DbSet.Remove(entity);
            _db.SaveChanges();
        }

        public void Remove(ICollection<T> entities) {
            DbSet.RemoveRange(entities);
            _db.SaveChanges();
        }

        public void Update(T entity) {
            DbSet.Update(entity);
            _db.SaveChanges();
        }
    }
}
