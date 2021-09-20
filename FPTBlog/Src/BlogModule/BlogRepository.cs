using System.Linq;
using FPTBlog.Src.BlogModule.Entity;
using FPTBlog.Src.BlogModule.Interface;
using FPTBlog.Utils;

namespace FPTBlog.Src.BlogModule
{
    public class BlogRepository : IBlogRepository
    {
        private readonly DB Db;
        public BlogRepository(DB db)
        {
            this.Db = db;
        }
        public Blog GetBlogByBlogId(string blogId)
        {
            Blog blog = this.Db.Blog.FirstOrDefault(item => item.BlogId == blogId);
            return blog;
        }

        public bool SaveBlog(Blog blog)
        {
            this.Db.Blog.Add(blog);
            return this.Db.SaveChanges() > 0;
        }

        public bool UpdateBlog(Blog blog)
        {
            Blog obj = this.Db.Blog.FirstOrDefault(item => item.BlogId == blog.BlogId);
            if (obj == null)
            {
                return false;
            }

            obj.Title = blog.Title;
            obj.Content = blog.Content;

            return this.Db.SaveChanges() > 0;
        }
    }
}