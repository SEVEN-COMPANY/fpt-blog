using System.Collections.Generic;
using FPTBlog.BlogModule.Entity;

namespace FPTBlog.BlogModule.Interface
{
    public interface IBlogRepository
    {
        public Blog GetBlogByBlogId(string blogId);
        public bool SaveBlog(Blog blog);
        public bool UpdateBlog(Blog blog);
        public List<Blog> GetBlogs();
        public List<Blog> GetBlogsByCategory(string categoryId);
    }
}