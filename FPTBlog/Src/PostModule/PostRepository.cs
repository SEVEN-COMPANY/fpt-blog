
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
            this.Db.SaveChanges();
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

        public (List<Post>, int) GetPostsOfStudentWithStatus(int pageSize, int pageIndex, string studentId) {
            var query = this.GetAll(item => item.StudentId == studentId && item.Status != PostStatus.APPROVED, includeProperties: "Category").OrderBy(item => item.Status);


            List<Post> list = query.Take((pageIndex + 1) * pageSize).Skip(pageIndex * pageSize).ToList();
            int count = query.Count();
            return (list, count);

        }

        public (List<Post>, int) GetWaitPostsWithCount() {
            List<Post> posts = this.GetAll(item => item.Status == PostStatus.WAIT).ToList();
            return (posts, posts.Count);
        }

        private void AddLikeOrDislikePost(Post post, User user, Expression expression) {
            LikePost likeOrDislike = new LikePost();
            likeOrDislike.PostId = post.PostId;
            likeOrDislike.Post = post;
            likeOrDislike.UserId = user.UserId;
            likeOrDislike.User = user;
            likeOrDislike.Expression = expression;

            this.Db.LikePost.Add(likeOrDislike);
            this.Db.SaveChanges();
        }

        public void LikePost(Post post, User user) {
            LikePost obj = this.Db.LikePost.FirstOrDefault(item => item.PostId == post.PostId && item.UserId == user.UserId);

            if (obj == null) {
                post.Like += 1;
                this.Db.Post.Update(post);
                this.AddLikeOrDislikePost(post, user, Expression.LIKE);
                this.Db.SaveChanges();
                return;
            }

            if (obj != null && obj.Expression == Expression.DISLIKE) {
                post.Dislike -= 1;
                post.Like += 1;

                this.Db.Post.Update(post);
                this.Db.LikePost.Remove(obj);
                this.AddLikeOrDislikePost(post, user, Expression.LIKE);
                this.Db.SaveChanges();
                return;
            }

            if (obj != null && obj.Expression == Expression.LIKE) {
                post.Like -= 1;
                this.Db.Post.Update(post);
                this.Db.LikePost.Remove(obj);
                this.Db.SaveChanges();
                return;
            }
        }

        public void DislikePost(Post post, User user) {
            LikePost obj = this.Db.LikePost.FirstOrDefault(item => item.PostId == post.PostId && item.UserId == user.UserId);
            if (obj == null) {
                post.Dislike += 1;
                this.Db.Post.Update(post);
                this.AddLikeOrDislikePost(post, user, Expression.DISLIKE);
                this.Db.SaveChanges();
                return;
            }

            if (obj != null && obj.Expression == Expression.LIKE) {
                post.Like -= 1;
                post.Dislike += 1;

                this.Db.Post.Update(post);
                this.Db.LikePost.Remove(obj);
                this.AddLikeOrDislikePost(post, user, Expression.DISLIKE);
                this.Db.SaveChanges();
                return;
            }

            if (obj != null && obj.Expression == Expression.DISLIKE) {
                post.Dislike -= 1;
                this.Db.Post.Update(post);
                this.Db.LikePost.Remove(obj);
                this.Db.SaveChanges();

                return;
            }
        }

        public Report GetMonthlyReport() {
            Report report = new Report();
            report.PostThisMonth = 0;
            report.ViewThisMonth = 0;
            report.InteractThisMonth = 0;
            report.UserThisMonth = 0;
            report.PostLastMonth = 0;
            report.ViewLastMonth = 0;
            report.InteractLastMonth = 0;
            report.UserLastMonth = 0;
            string thisMonth = DateTime.Now.AddMonths(-1).ToShortDateString();
            DateTime thisMonthDate = Convert.ToDateTime(thisMonth);
            string lastMonth = DateTime.Now.AddMonths(-2).ToShortDateString();
            DateTime lastMonthDate = Convert.ToDateTime(lastMonth);

            List<Post> posts = this.Db.Post.ToList();
            List<User> users = this.Db.User.ToList();
            foreach (var post in posts) {
                DateTime date = Convert.ToDateTime(post.CreateDate);
                if (DateTime.Compare(date, thisMonthDate) > 0) {
                    report.PostThisMonth++;
                    report.ViewThisMonth += post.View;
                    report.InteractThisMonth += post.Like + post.Dislike + this.Db.Comment.Where(x => x.PostId == post.PostId).Count();
                }
                if (DateTime.Compare(date, lastMonthDate) > 0 && (DateTime.Compare(date, thisMonthDate) < 0)) {
                    report.PostLastMonth++;
                    report.ViewLastMonth += post.View;
                    report.InteractLastMonth += post.Like + post.Dislike + this.Db.Comment.Where(x => x.PostId == post.PostId).Count();
                }
            }

            foreach (var user in users) {
                DateTime date = Convert.ToDateTime(user.CreateDate);
                if (DateTime.Compare(date, thisMonthDate) > 0) {
                    report.UserThisMonth++;
                }
                if (DateTime.Compare(date, lastMonthDate) > 0 && (DateTime.Compare(date, thisMonthDate) < 0)) {
                    report.UserLastMonth++;
                }
            }
            return report;
        }

        public (List<Post>, int) GetPostsByStatus(int pageSize, int pageIndex, string search, PostStatus status) {

            var query = (from Post in this.Db.Post
                         where ((Post.Student.Name.Contains(search) || Post.Student.Username.Contains(search) || Post.Title.Contains(search))) && Post.Status == status && Post.Status != PostStatus.DRAFT
                         select Post);

            List<Post> list = query.Take((pageIndex + 1) * pageSize).Skip(pageIndex * pageSize).ToList();
            foreach (var post in list) {
                this.Db.Entry(post).Reference(item => item.Student).Load();
            }
            int count = query.Count();
            return (list, count);

        }

        public (List<Post>, int) GetAllPosts(int pageSize, int pageIndex, string search) {
            var query = (from Post in this.Db.Post
                         where ((Post.Student.Name.Contains(search) || Post.Student.Username.Contains(search) || Post.Title.Contains(search)) && Post.Status != PostStatus.DRAFT)
                         select Post);

            List<Post> list = query.Take((pageIndex + 1) * pageSize).Skip(pageIndex * pageSize).ToList();
            foreach (var post in list) {
                this.Db.Entry(post).Reference(item => item.Student).Load();
            }
            int count = query.Count();
            return (list, count);

        }

        public List<PostChart> GetPostChart() {
            var today = DateTime.Now.Date;
            List<PostChart> chart = new List<PostChart>();
            for (int i = -29; i <= 0; i++) {
                // var users = this.Db.User.Where(x => Convert.ToDateTime(x.CreateDate) == today.AddDays(i)).Count();
                // var interacts = this.Db.LikePost.Where(x => Convert.ToDateTime(x.CreateDate) == today.AddDays(i)).Count()
                // + this.Db.Comment.Where(x => Convert.ToDateTime(x.CreateDate) == today.AddDays(i)).Count();
                var posts = (from Post in this.Db.Post
                             select Post).ToList()
                             .Where(x => Convert.ToDateTime(x.CreateDate) == today.AddDays(i))
                             .ToList();

                var users = (from User in this.Db.User
                             select User).ToList()
                .Where(x => Convert.ToDateTime(x.CreateDate) == today.AddDays(i))
                .ToList();

                PostChart postChart = new PostChart();

                postChart.Post = posts.Count;
                postChart.View = posts.Sum(x => x.View);
                postChart.User = users.Count;
                postChart.date = today.AddDays(i).ToShortDateString();
                chart.Add(postChart);

            }
            return chart;
        }
    }
}
