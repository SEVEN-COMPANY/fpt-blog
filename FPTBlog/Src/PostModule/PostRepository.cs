using System.Collections.Generic;
using System.Linq;
using FPTBlog.Src.BlogModule.Entity;
using FPTBlog.Src.BlogModule.Interface;
using FPTBlog.Src.TagModule.Entity;
using FPTBlog.Src.UserModule.Entity;
using FPTBlog.Utils;
using FPTBlog.Utils.Repository;

namespace FPTBlog.Src.BlogModule {
    public class PostRepository : Repository<Post>, IPostRepository {
        private readonly DB Db;
        public PostRepository(DB db) : base(db) {
            this.Db = db;
        }


        public bool AddTagToPost(Post blog, Tag tag) {
            PostTag blogTag = new PostTag();
            blogTag.PostId = blog.PostId;
            blogTag.post = blog;
            blogTag.TagId = tag.TagId;
            blogTag.Tag = tag;

            blog.BlogTags.Add(blogTag);
            tag.BlogTags.Add(blogTag);
            this.Db.PostTag.Add(blogTag);

            return this.Db.SaveChanges() > 0;
        }



        public List<Tag> GetTagsFromPost(Post blog) {
            List<Tag> tags = (from Blog in this.Db.Post
                              where Blog.PostId.Equals(blog.PostId)
                              join BlogTag in this.Db.PostTag
                              on Blog.PostId equals BlogTag.PostId
                              join Tag in this.Db.Tag
                              on BlogTag.TagId equals Tag.TagId
                              select Tag).ToList();
            return tags;
        }

        public void RemoveTagFromPost(Post blog, Tag tag) {
            PostTag blogTag = this.Db.PostTag.FirstOrDefault(item => item.PostId == blog.PostId && item.TagId == tag.TagId);

            this.Db.BlogTag.Remove(blogTag);
        }


        public (List<Post>, int) GetPostsWithCount(int pageSize, int pageIndex) {
            var query = (from Blog in this.Db.Post
                         orderby Blog.Like - Blog.Dislike + (Blog.View / 10)
                         select Blog);
            return this.GetEntityByPage(query, pageSize, pageIndex);
        }

        public (List<Post>, int) GetPostsByTagWithCount(int pageSize, int pageIndex, string name) {
            var query = (from BlogTag in this.Db.PostTag
                         join Tag in this.Db.Tag
                         on BlogTag.TagId equals Tag.TagId
                         join Blog in this.Db.Post
                         on BlogTag.PostId equals Blog.PostId
                         where Tag.Name.Equals(name)
                         select Blog);
            return this.GetEntityByPage(query, pageSize, pageIndex);
        }

        public (List<Post>, int) GetPostsByCategoryWithCount(int pageSize, int pageIndex, string name) {
            var query = (from Category in this.Db.Category
                         join Post in this.Db.Post
                         on Category.CategoryId equals Post.CategoryId
                         where Category.Name.Equals(name)
                         select Post);
            return this.GetEntityByPage(query, pageSize, pageIndex);
        }

        public (List<Post>, int) GetPostsOfStudentWithStatus(int pageSize, int pageIndex, string studentId, PostStatus status) {
            var query = (from Blog in this.Db.Post
                         where Blog.StudentId.Equals(studentId) && Blog.Status == status
                         select Blog);

            return this.GetEntityByPage(query, pageSize, pageIndex);
        }

        private (List<Post>, int) GetPostsWithCount(IQueryable<Post> query, int pageSize, int pageIndex) {
            List<Post> blogs = query.Take((pageIndex + 1) * pageSize).Skip(pageIndex * pageSize).ToList();
            int count = query.Count();


            return (blogs, count);
        }

        public void LikeBlog(Post blog, User user) {
            LikePost obj = this.Db.LikeBlog.FirstOrDefault(item => item.PostId == blog.PostId && item.UserId == user.UserId);
            if (obj == null) {
                blog.Like += 1;
                this.Db.Post.Update(blog);
                LikePost like = new LikePost();
                like.PostId = blog.PostId;
                like.Post = blog;
                like.UserId = user.UserId;
                like.User = user;
                this.Db.LikeBlog.Add(like);
                return;
            }

            blog.Like -= 1;
            this.Db.Post.Update(blog);
            this.Db.LikeBlog.Remove(obj);
            return;

        }

        public (List<Post>, int) GetWaitBlogAndCount() {
            List<Post> blogs = this.Db.Post.Where(item => (item.Status) == PostStatus.WAIT).ToList();
            return (blogs, blogs.Count);
        }
    }
}
