using System.Collections.Generic;
using FPTBlog.BlogModule.Entity;
using FPTBlog.BlogModule.Interface;

namespace FPTBlog.BlogModule
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository BlogRepository;
        public BlogService(IBlogRepository blogRepository){
            this.BlogRepository = blogRepository;
        }

        public List<Blog> GetBlogs()
        {
            return this.BlogRepository.GetBlogs();
        }

        public Blog GetBlogByBlogId(string blogId)
        {
            return this.BlogRepository.GetBlogByBlogId(blogId);
        }

        public bool SaveBlog(Blog blog)
        {
            return this.BlogRepository.SaveBlog(blog);
        }

        public bool UpdateBlog(Blog blog)
        {
            return this.BlogRepository.UpdateBlog(blog);
        }

        public List<Blog> GetBlogsByCategory(string categoryId)
        {
            return this.BlogRepository.GetBlogsByCategory(categoryId);
        }
    }
}