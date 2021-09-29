
using System.Collections.Generic;
using System.Linq;
using FPTBlog.Src.PostModule.Entity;
using FPTBlog.Src.PostModule.Interface;
using FPTBlog.Src.TagModule.Entity;
using FPTBlog.Utils;
using FPTBlog.Utils.Repository;

namespace FPTBlog.Src.PostModule {
    public class PostRepository : Repository<Post>, IPostRepository {
        private readonly DB Db;
        public PostRepository(DB db) : base(db) {
            this.Db = db;
        }

        public void AddTagToPost(Post blog, Tag tag) {
            PostTag postTag = new PostTag();
            postTag.PostId = blog.PostId;
            postTag.post = blog;
            postTag.TagId = tag.TagId;
            postTag.Tag = tag;

            blog.PostTags.Add(postTag);
            tag.PostTags.Add(postTag);
            this.Db.PostTag.Add(postTag);

            this.Db.SaveChanges();
        }

        public void RemoveTagFromPost(Post post, Tag tag) {
            PostTag blogTag = this.Db.PostTag.FirstOrDefault(item => item.PostId == post.PostId && item.TagId == tag.TagId);

            this.Db.PostTag.Remove(blogTag);
        }

        public List<Tag> GetTagsFromPost(Post blog) {
            List<Tag> tags = (from Post in this.Db.Post
                              where Post.PostId.Equals(blog.PostId)
                              join PostTag in this.Db.PostTag
                              on Post.PostId equals PostTag.PostId
                              join Tag in this.Db.Tag
                              on PostTag.TagId equals Tag.TagId
                              select Tag).ToList();
            return tags;
        }
        public (List<Post>, int) GetPostsByCategoryWithCount(int pageSize, int pageIndex, string name) {
            var query = (from Category in this.Db.Category
                         join Post in this.Db.Post
                         on Category.CategoryId equals Post.CategoryId
                         where Category.Name.Equals(name)
                         select Post);

            List<Post> list = query.Take((pageIndex + 1) * pageSize).Skip(pageIndex * pageSize).ToList();
            int count = query.Count();
            return (list, count);
            // return this.GetEntityByPage(query, pageSize, pageIndex);
        }
        public (List<Post>, int) GetPostsByTagWithCount(int pageSize, int pageIndex, string name) {
            var query = (from BlogTag in this.Db.PostTag
                         join Tag in this.Db.Tag
                         on BlogTag.TagId equals Tag.TagId
                         join Blog in this.Db.Post
                         on BlogTag.PostId equals Blog.PostId
                         where Tag.Name.Equals(name)
                         select Blog);

            List<Post> list = query.Take((pageIndex + 1) * pageSize).Skip(pageIndex * pageSize).ToList();
            int count = query.Count();
            return (list, count);

        }

        public (List<Post>, int) GetPostsOfStudentWithStatus(int pageSize, int pageIndex, string studentId, PostStatus status) {
            var query = (from Blog in this.Db.Post
                         where Blog.StudentId.Equals(studentId) && Blog.Status == status
                         select Blog);


            List<Post> list = query.Take((pageIndex + 1) * pageSize).Skip(pageIndex * pageSize).ToList();
            int count = query.Count();
            return (list, count);

        }

        public (List<Post>, int) GetWaitPostsWithCount() {
            List<Post> blogs = this.Db.Post.Where(item => (item.Status) == PostStatus.WAIT).ToList();
            return (blogs, blogs.Count);
        }

    }
}
