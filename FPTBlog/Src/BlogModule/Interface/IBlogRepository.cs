using System.Collections.Generic;
using FPTBlog.Src.BlogModule.Entity;
using FPTBlog.Src.TagModule.Entity;

namespace FPTBlog.Src.BlogModule.Interface
{
    public interface IBlogRepository
    {
        public Blog GetBlogByBlogId(string blogId);
        public bool SaveBlog(Blog blog);
        public bool UpdateBlog(Blog blog);
        public List<Tag> GetTagFromBlog(Blog blog);
        public bool RemoveTagFromBlog(List<Tag> tags);
        public bool AddTagToBlog(Blog blog, List<Tag> tags);
        public (List<Blog>, int) GetBlogsByTagAndCount(int pageSize, int pageIndex, string name);
        public (List<Blog>, int) GetBlogsByCategoryAndCount(int pageSize, int pageIndex, string name);
        public (List<Blog>, int) GetAllBlogsAndCount(int pageSize, int pageIndex);
    }
}