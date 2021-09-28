using System.Collections.Generic;
using System.Linq;
using FPTBlog.Src.BlogModule.Entity;
using FPTBlog.Src.BlogModule.Interface;
using FPTBlog.Src.TagModule.Entity;
using FPTBlog.Src.UserModule.Entity;
using FPTBlog.Utils;

namespace FPTBlog.Src.BlogModule {
    public class BlogRepository : IBlogRepository {
        private readonly DB Db;
        public BlogRepository(DB db) {
            this.Db = db;
        }

        public List<BlogTag> GetBlogTags() {
            List<BlogTag> list = this.Db.BlogTag.ToList();
            return list;
        }


        public bool AddTagToBlog(Blog blog, Tag tag) {
            BlogTag blogTag = new BlogTag();
            blogTag.BlogId = blog.BlogId;
            blogTag.Blog = blog;
            blogTag.TagId = tag.TagId;
            blogTag.Tag = tag;

            blog.BlogTags.Add(blogTag);
            tag.BlogTags.Add(blogTag);
            this.Db.BlogTag.Add(blogTag);

            return this.Db.SaveChanges() > 0;
        }

        public Blog GetBlogByBlogId(string blogId) {
            Blog blog = this.Db.Blog.FirstOrDefault(item => item.BlogId == blogId);
            return blog;
        }

        public List<Tag> GetTagsFromBlog(Blog blog) {
            List<Tag> tags = (from Blog in this.Db.Blog
                              where Blog.BlogId.Equals(blog.BlogId)
                              join BlogTag in this.Db.BlogTag
                              on Blog.BlogId equals BlogTag.BlogId
                              join Tag in this.Db.Tag
                              on BlogTag.TagId equals Tag.TagId
                              select Tag).ToList();
            return tags;
        }

        public bool RemoveTagFromBlog(Blog blog, Tag tag) {
            BlogTag blogTag = this.Db.BlogTag.FirstOrDefault(item => item.BlogId == blog.BlogId && item.TagId == tag.TagId);

            this.Db.BlogTag.Remove(blogTag);

            return this.Db.SaveChanges() > 0;
        }

        public bool SaveBlog(Blog blog) {
            this.Db.Blog.Add(blog);
            return this.Db.SaveChanges() > 0;
        }

        public bool UpdateBlog(Blog blog) {
            Blog obj = this.Db.Blog.FirstOrDefault(item => item.BlogId == blog.BlogId);
            if (obj == null) {
                return false;
            }

            obj.Title = blog.Title;
            obj.Content = blog.Content;

            return this.Db.SaveChanges() > 0;
        }
        public (List<Blog>, int) GetAllBlogsAndCount(int pageSize, int pageIndex) {
            var query = (from Blog in this.Db.Blog
                         orderby Blog.Like - Blog.Dislike + (Blog.View / 10)
                         select Blog);
            return this.GetBlogsAndCountFromQuery(query, pageSize, pageIndex);
        }

        public (List<Blog>, int) GetBlogsByTagAndCount(int pageSize, int pageIndex, string name) {
            var query = (from BlogTag in this.Db.BlogTag
                         join Tag in this.Db.Tag
                         on BlogTag.TagId equals Tag.TagId
                         join Blog in this.Db.Blog
                         on BlogTag.BlogId equals Blog.BlogId
                         where Tag.Name.Equals(name)
                         select Blog);
            return this.GetBlogsAndCountFromQuery(query, pageSize, pageIndex);
        }

        public (List<Blog>, int) GetBlogsByCategoryAndCount(int pageSize, int pageIndex, string name) {
            var query = (from Category in this.Db.Category
                         join Blog in this.Db.Blog
                         on Category.CategoryId equals Blog.CategoryId
                         where Category.Name.Equals(name)
                         select Blog);
            return this.GetBlogsAndCountFromQuery(query, pageSize, pageIndex);
        }

        public (List<Blog>, int) GetBlogsOfStudentWithStatus(int pageSize, int pageIndex, string studentId, BlogStatus status) {
            var query = (from Blog in this.Db.Blog
                         where Blog.StudentId.Equals(studentId) && Blog.Status == status
                         select Blog);

            return this.GetBlogsAndCountFromQuery(query, pageSize, pageIndex);
        }

        private (List<Blog>, int) GetBlogsAndCountFromQuery(IQueryable<Blog> query, int pageSize, int pageIndex) {
            List<Blog> blogs = query.Take((pageIndex + 1) * pageSize).Skip(pageIndex * pageSize).ToList();
            int count = query.Count();
            return (blogs, count);
        }

        public bool LikeBlog(Blog blog, User user) {
            LikeBlog obj = this.Db.LikeBlog.FirstOrDefault(item => item.BlogId == blog.BlogId && item.UserId == user.UserId);
            if (obj == null) {
                blog.Like += 1;
                this.Db.Blog.Update(blog);
                LikeBlog like = new LikeBlog();
                like.BlogId = blog.BlogId;
                like.Blog = blog;
                like.UserId = user.UserId;
                like.User = user;
                this.Db.LikeBlog.Add(like);
                return this.Db.SaveChanges() > 0;
            }

            blog.Like -= 1;
            this.Db.Blog.Update(blog);
            this.Db.LikeBlog.Remove(obj);
            return this.Db.SaveChanges() > 0;

        }
    }
}
