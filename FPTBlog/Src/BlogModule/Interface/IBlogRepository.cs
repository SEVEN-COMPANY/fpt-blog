using FPTBlog.Src.BlogModule.Entity;

namespace FPTBlog.Src.BlogModule.Interface
{
    public interface IBlogRepository
    {
        public Blog GetBlogByBlogId(string blogId);
        public bool SaveBlog(Blog blog);
        public bool UpdateBlog(Blog blog);
    }
}