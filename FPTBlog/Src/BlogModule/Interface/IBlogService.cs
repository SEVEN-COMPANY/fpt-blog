using FPTBlog.Src.BlogModule.Entity;

namespace FPTBlog.Src.BlogModule.Interface
{
    public interface IBlogService
    {
        public Blog GetBlogByBlogId(string blogId);
        public bool SaveBlog(Blog blog);
        public bool UpdateBlog(Blog blog);
    }
}