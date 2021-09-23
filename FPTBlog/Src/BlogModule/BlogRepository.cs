using System;
using System.Collections.Generic;
using System.Linq;
using FPTBlog.Src.BlogModule.Entity;
using FPTBlog.Src.BlogModule.Interface;
using FPTBlog.Src.TagModule.Entity;
using FPTBlog.Utils;

namespace FPTBlog.Src.BlogModule
{
    public class BlogRepository : IBlogRepository
    {
        private readonly DB Db;
        public BlogRepository(DB db)
        {
            this.Db = db;
        }

        public bool AddTagToBlog(Blog blog, List<Tag> tags)
        {
            List<BlogTag> blogTags = new List<BlogTag>();
            foreach (Tag tag in tags)
            {
                BlogTag blogTag = new BlogTag();
                blogTag.BlogId = blog.BlogId;
                blogTag.Blog = blog;
                blogTag.TagId = tag.TagId;
                blogTag.Tag = tag;
                blogTags.Add(blogTag);
            }
            this.Db.BlogTag.AddRange(blogTags);
            return this.Db.SaveChanges() > 0;
        }

        public Blog GetBlogByBlogId(string blogId)
        {
            Blog blog = this.Db.Blog.FirstOrDefault(item => item.BlogId == blogId);
            return blog;
        }

        public List<Tag> GetTagFromBlog(Blog blog)
        {
            List<Tag> tags = (from Tag in this.Db.Tag
                              where Tag.BlogTags.Any(bt => bt.BlogId == blog.BlogId)
                              select Tag).ToList();
            return tags;
        }

        public bool RemoveTagFromBlog(List<Tag> tags)
        {
            List<BlogTag> blogTags = (from BlogTag in this.Db.BlogTag
                                      where tags.Contains(BlogTag.Tag)
                                      select BlogTag).ToList();

            this.Db.BlogTag.RemoveRange(blogTags);

            return this.Db.SaveChanges() > 0;
        }

        public bool SaveBlog(Blog blog)
        {
            this.Db.Blog.Add(blog);
            return this.Db.SaveChanges() > 0;
        }

        public bool UpdateBlog(Blog blog)
        {
            Blog obj = this.Db.Blog.FirstOrDefault(item => item.BlogId == blog.BlogId);
            if (obj == null)
            {
                return false;
            }

            obj.Title = blog.Title;
            obj.Content = blog.Content;

            return this.Db.SaveChanges() > 0;
        }

        public (List<Blog>, int) GetBlogsByTagAndCount(int currentPage, int pageSize, string name)
        {
            var query = (from BlogTag in this.Db.BlogTag
                         join Tag in this.Db.Tag
                         on BlogTag.TagId equals Tag.TagId
                         join Blog in this.Db.Blog
                         on BlogTag.BlogId equals Blog.BlogId
                         where Tag.Name.Equals(name)
                         select Blog);
            List<Blog> blogs = query.Take((pageSize + 1) * currentPage).Skip(currentPage * pageSize).ToList();
            int count = query.Count();
            return (blogs, count);
        }
        public (List<Blog>, int) GetAllBlogsAndCount(int currentPage, int pageSize)
        {
            var query = (from Blog in this.Db.Blog
                        orderby Blog.Like - Blog.Dislike + (Blog.View / 10)
                        select Blog);
            var blogs = query.Take((pageSize + 1) * currentPage).Skip(currentPage * pageSize).ToList();
            int count = query.Count();
            return (blogs, count);
        }
    }
}