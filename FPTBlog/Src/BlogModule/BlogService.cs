using FPTBlog.Src.BlogModule.Entity;
using FPTBlog.Src.BlogModule.Interface;

namespace FPTBlog.Src.BlogModule
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository BlogRepository;
        public BlogService(IBlogRepository blogRepository)
        {
            this.BlogRepository = blogRepository;
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
    }
}