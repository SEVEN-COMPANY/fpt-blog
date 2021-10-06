
using System;
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
            post.PostTags.Remove(postTag);
            this.Update(post);
            tag.PostTags.Remove(postTag);
            this.Db.Tag.Update(tag);
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

        }

        public (List<Post>, int) GetPostsOfStudentWithStatus(int pageSize, int pageIndex, string studentId, PostStatus status) {
            var query = (from Post in this.Db.Post
                         where Post.StudentId.Equals(studentId) && Post.Status == status
                         select Post);

            List<Post> list = query.Take((pageIndex + 1) * pageSize).Skip(pageIndex * pageSize).ToList();
            int count = query.Count();
            return (list, count);

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






        public void LikePost(Post post, User user) {
            LikePost obj = this.Db.LikePost.FirstOrDefault(item => item.PostId == post.PostId && item.UserId == user.UserId);
            if (obj == null || (obj != null && (int) obj.expression == 2)) {
                if (obj != null && (int) obj.expression == 2) {
                    post.Dislike -= 1;
                    this.Db.Post.Update(post);
                    this.Db.LikePost.Remove(obj);
                }
                post.Like += 1;
                this.Db.Post.Update(post);
                LikePost like = new LikePost();
                like.LikePostId = Guid.NewGuid().ToString();
                like.PostId = post.PostId;
                like.Post = post;
                like.UserId = user.UserId;
                like.User = user;
                like.expression = Expression.LIKE;
                this.Db.LikePost.Add(like);
                this.Db.SaveChanges();
                return;
            }
            if (obj != null && (int) obj.expression == 1) {
                post.Like -= 1;
                this.Db.Post.Update(post);
                this.Db.LikePost.Remove(obj);
                this.Db.SaveChanges();
                return;
            }
        }

        public void DislikePost(Post post, User user) {
            LikePost obj = this.Db.LikePost.FirstOrDefault(item => item.PostId == post.PostId && item.UserId == user.UserId);
            if (obj == null || (obj != null && (int) obj.expression == 1)) {
                if (obj != null && (int) obj.expression == 1) {
                    post.Like -= 1;
                    this.Db.Post.Update(post);
                    this.Db.LikePost.Remove(obj);
                }
                post.Dislike += 1;
                this.Db.Post.Update(post);
                LikePost dislike = new LikePost();
                dislike.LikePostId = Guid.NewGuid().ToString();
                dislike.PostId = post.PostId;
                dislike.Post = post;
                dislike.UserId = user.UserId;
                dislike.User = user;
                dislike.expression = Expression.DISLIKE;
                this.Db.LikePost.Add(dislike);
                this.Db.SaveChanges();
                return;
            }
            if (obj != null && (int) obj.expression == 2) {
                post.Dislike -= 1;
                this.Db.Post.Update(post);
                this.Db.LikePost.Remove(obj);
                this.Db.SaveChanges();
                return;
            }
        }
    }
}
