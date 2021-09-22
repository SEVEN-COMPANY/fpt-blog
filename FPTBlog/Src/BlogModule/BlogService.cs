using System.Collections.Generic;
using FPTBlog.Src.BlogModule.Entity;
using FPTBlog.Src.BlogModule.Interface;
using FPTBlog.Src.TagModule.Entity;

namespace FPTBlog.Src.BlogModule
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository BlogRepository;
        public BlogService(IBlogRepository blogRepository)
        {
            this.BlogRepository = blogRepository;
        }

        public bool AddTagToBlog(Blog blog, List<Tag> tags)
        {
            return this.BlogRepository.AddTagToBlog(blog, tags);
        }

        public Blog GetBlogByBlogId(string blogId)
        {
            return this.BlogRepository.GetBlogByBlogId(blogId);
        }

        public List<Tag> GetTagFromBlog(Blog blog)
        {
            return this.BlogRepository.GetTagFromBlog(blog);
        }

        public bool RemoveTagFromBlog(List<Tag> tags)
        {
            return this.BlogRepository.RemoveTagFromBlog(tags);
        }

        public bool SaveBlog(Blog blog)
        {
            return this.BlogRepository.SaveBlog(blog);
        }

        public bool UpdateBlog(Blog blog)
        {
            return this.BlogRepository.UpdateBlog(blog);
        }

        public int CalculateBlogPoint(Blog blog)
        {
            int result = blog.Like - blog.Dislike + (blog.View / 10);
            return result;
        }
    }
}