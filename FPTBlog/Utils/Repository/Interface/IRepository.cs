using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FPTBlog.Utils.Repository.Interface {
    public interface IRepository<T> where T : class {
        T Get(string key);

        void Add(T entity);

        ICollection<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, ICollection<T>> options = null,
            string includeProperties = null
            );
        T GetFirstOrDefault(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null
            );

        void Update(T entity);

        void Remove(string key);

        void Remove(T entity);

        void Remove(ICollection<T> entities);

        // IEnumerable<T> GetEntityByPage(IEnumerable<T> enumrable, int pageSize, int pageIndex);

    }
}
