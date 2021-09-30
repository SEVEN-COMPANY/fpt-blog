using System.Collections.Generic;
using System.Linq;
using FPTBlog.Src.PostModule.Entity;
using FPTBlog.Src.PostModule.Interface;
using FPTBlog.Src.TagModule.Entity;
using FPTBlog.Src.UserModule.Entity;
using FPTBlog.Utils;
using FPTBlog.Utils.Repository;

namespace FPTBlog.Src.PostModule {
    public class PostRepository : Repository<Post>, IPostRepository {
        private readonly DB Db;
        public PostRepository(DB db) : base(db) {
            this.Db = db;
        }

        public void AddTagToPost(Post post, Tag tag) {
            PostTag postTag = new PostTag();
            postTag.PostId = post.PostId;
            postTag.Post = post;
            postTag.TagId = tag.TagId;
            postTag.Tag = tag;

            post.PostTags.Add(postTag);
            tag.PostTags.Add(postTag);
            this.Db.PostTag.Add(postTag);

            this.Db.SaveChanges();
        }

        public void RemoveTagFromPost(Post post, Tag tag) {
            PostTag postTag = this.Db.PostTag.FirstOrDefault(item => item.PostId == post.PostId && item.TagId == tag.TagId);

            this.Db.PostTag.Remove(postTag);
        }

        public List<Tag> GetTagsFromPost(Post post) {
            List<Tag> tags = (from Post in this.Db.Post
                              where Post.PostId.Equals(post.PostId)
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
            var query = (from PostTag in this.Db.PostTag
                         join Tag in this.Db.Tag
                         on PostTag.TagId equals Tag.TagId
                         join Post in this.Db.Post
                         on PostTag.PostId equals Post.PostId
                         where Tag.Name.Equals(name)
                         select Post);

            List<Post> list = query.Take((pageIndex + 1) * pageSize).Skip(pageIndex * pageSize).ToList();
            int count = query.Count();
            return (list, count);
            // return this.GetEntityByPage(query, pageSize, pageIndex);
        }

        public (List<Post>, int) GetPostsOfStudentWithStatus(int pageSize, int pageIndex, string studentId, PostStatus status) {
            var query = (from Post in this.Db.Post
                        where Post.StudentId.Equals(studentId) && Post.Status == status
                        select Post);

            List<Post> list = query.Take((pageIndex + 1) * pageSize).Skip(pageIndex * pageSize).ToList();
            int count = query.Count();
            return (list, count);
            // return this.GetEntityByPage(query, pageSize, pageIndex);
        }

        public (List<Post>, int) GetWaitPostsWithCount() {
            List<Post> posts = this.GetAll(item => item.Status == PostStatus.WAIT).ToList();
            return (posts, posts.Count);
        }

        // private (List<Post>, int) GetPostsWithCount(IQueryable<Post> query, int pageSize, int pageIndex) {
        //     List<Post> blogs = query.Take((pageIndex + 1) * pageSize).Skip(pageIndex * pageSize).ToList();
        //     int count = query.Count();


        //     return (blogs, count);
        // }


        //     public (List<Post>, int) GetPostsWithCount(int pageSize, int pageIndex) {
        //         var query = (from Blog in this.Db.Post
        //                      orderby Blog.Like - Blog.Dislike + (Blog.View / 10)
        //                      select Blog);
        //         return this.GetEntityByPage(query, pageSize, pageIndex);
        //     }






        //     public void LikeBlog(Post blog, User user) {
        //         LikePost obj = this.Db.LikeBlog.FirstOrDefault(item => item.PostId == blog.PostId && item.UserId == user.UserId);
        //         if (obj == null) {
        //             blog.Like += 1;
        //             this.Db.Post.Update(blog);
        //             LikePost like = new LikePost();
        //             like.PostId = blog.PostId;
        //             like.Post = blog;
        //             like.UserId = user.UserId;
        //             like.User = user;
        //             this.Db.LikeBlog.Add(like);
        //             return;
        //         }

        //         blog.Like -= 1;
        //         this.Db.Post.Update(blog);
        //         this.Db.LikeBlog.Remove(obj);
        //         return;

        //     }

    }
}
