using FPTBlog.Src.CategoryModule.Entity;
using FPTBlog.Utils.Repository.Interface;

namespace FPTBlog.Utils.Repository
{
    public class NewCategoryRepository : Repository<Category>, INewCategoryRepository
    {
        private readonly DB _db;
        public NewCategoryRepository(DB db) : base(db)
        {
            _db = db;
            DbSet = _db.Category;
        }
    }
}
