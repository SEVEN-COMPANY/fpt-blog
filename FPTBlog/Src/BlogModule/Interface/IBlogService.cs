using System.Collections.Generic;
using FPTBlog.Src.BlogModule.Entity;
using FPTBlog.Src.TagModule.Entity;
using FPTBlog.Src.UserModule.Entity;

namespace FPTBlog.Src.BlogModule.Interface {
    public interface IBlogService {
        public Blog GetBlogByBlogId(string blogId);
        public bool SaveBlog(Blog blog);
        public bool UpdateBlog(Blog blog);
        public List<Tag> GetTagsFromBlog(Blog blog);
        public bool RemoveTagFromBlog(Blog blog, Tag tag);
        public bool AddTagToBlog(Blog blog, Tag tag);
        public (List<Blog>, int) GetBlogsByTagAndCount(int pageSize, int pageIndex, string name);
        public (List<Blog>, int) GetBlogsByCategoryAndCount(int pageSize, int pageIndex, string name);
        public int CalculateBlogPoint(Blog blog);
        public (List<Blog>, int) GetAllBlogsAndCount(int pageSize, int pageIndex);
        public (List<Blog>, int) GetBlogsOfStudentWithStatus(int pageSize, int pageIndex, string studentId, BlogStatus status);
        public bool LikeBlog(Blog blog, User user);
    }
}
