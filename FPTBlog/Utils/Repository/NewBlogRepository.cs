using FPTBlog.Src.BlogModule.Entity;
using FPTBlog.Utils.Repository.Interface;

namespace FPTBlog.Utils.Repository
{
    public class NewBlogRepository : Repository<Blog>, INewBlogRepository
    {
        private readonly DB _db;
        public NewBlogRepository(DB db) : base(db)
        {
            _db = db;
            DbSet = _db.Blog;
        }
    }
}
