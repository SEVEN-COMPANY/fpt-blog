using System.Collections.Generic;
using FPTBlog.Src.BlogModule.Entity;
using FPTBlog.Src.BlogModule.Interface;
using FPTBlog.Src.TagModule.Entity;
using FPTBlog.Src.UserModule.Entity;

namespace FPTBlog.Src.BlogModule {
    public class BlogService : IBlogService {
        private readonly IBlogRepository BlogRepository;
        public BlogService(IBlogRepository blogRepository) {
            this.BlogRepository = blogRepository;
        }

        public bool AddTagToBlog(Blog blog, Tag tag) {
            return this.BlogRepository.AddTagToBlog(blog, tag);
        }

        public Blog GetBlogByBlogId(string blogId) {
            return this.BlogRepository.GetBlogByBlogId(blogId);
        }

        public List<Tag> GetTagsFromBlog(Blog blog) {
            return this.BlogRepository.GetTagsFromBlog(blog);
        }

        public bool RemoveTagFromBlog(Blog blog, Tag tag) {
            return this.BlogRepository.RemoveTagFromBlog(blog, tag);
        }

        public bool SaveBlog(Blog blog) {
            return this.BlogRepository.SaveBlog(blog);
        }

        public bool UpdateBlog(Blog blog) {
            return this.BlogRepository.UpdateBlog(blog);
        }

        public int CalculateBlogPoint(Blog blog) {
            int result = blog.Like - blog.Dislike + (blog.View / 10);
            return result;
        }

        public (List<Blog>, int) GetBlogsByTagAndCount(int pageSize, int pageIndex, string name) {
            return this.BlogRepository.GetBlogsByTagAndCount(pageSize, pageIndex, name);
        }
        public (List<Blog>, int) GetAllBlogsAndCount(int pageSize, int pageIndex) {
            return this.BlogRepository.GetAllBlogsAndCount(pageSize, pageIndex);
        }
        public (List<Blog>, int) GetBlogsByCategoryAndCount(int pageSize, int pageIndex, string name) {
            return this.BlogRepository.GetBlogsByCategoryAndCount(pageSize, pageIndex, name);
        }

        public (List<Blog>, int) GetBlogsOfStudentWithStatus(int pageSize, int pageIndex, string studentId, BlogStatus status) {
            return this.BlogRepository.GetBlogsOfStudentWithStatus(pageSize, pageIndex, studentId, status);
        }

        public bool LikeBlog(Blog blog, User user) {
            return this.BlogRepository.LikeBlog(blog, user);
        }

        public List<Blog> GetAllWaitBlogs() {
            return this.BlogRepository.GetAllWaitBlogs();
        }

    }
}
