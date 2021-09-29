using FPTBlog.Utils.Repository.Interface;

namespace FPTBlog.Utils.Repository
{
    public class UnitOfWork : IUnitOfWork {

        private readonly DB _db;
        public INewCategoryRepository Category {get;private set;}
        public INewBlogRepository Blog {get;private set;}
        public UnitOfWork(DB db)
        {
            _db = db;
            Category = new NewCategoryRepository(_db);
            Blog = new NewBlogRepository(_db);
        }

        public void SaveChange() {
            this._db.SaveChanges();
        }
    }
}
