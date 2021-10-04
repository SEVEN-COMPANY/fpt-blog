using System;
using System.Collections.Generic;
using System.Linq;
using FPTBlog.Src.CommentModule.Entity;
using FPTBlog.Src.CommentModule.Interface;
using FPTBlog.Src.PostModule.Entity;
using FPTBlog.Src.PostModule.Interface;
using FPTBlog.Src.TagModule.Entity;
using FPTBlog.Src.UserModule.Entity;

namespace FPTBlog.Src.PostModule {
    public class PostService : IPostService {
        private readonly IPostRepository PostRepository;
        private readonly ICommentRepository CommentRepository;
        public PostService(IPostRepository postRepository, ICommentRepository commentRepository) {
            this.PostRepository = postRepository;
            this.CommentRepository = commentRepository;
        }

        public void AddPost(Post post) => this.PostRepository.Add(post);
        public Post GetPostByPostId(string postId) => this.PostRepository.GetFirstOrDefault(item => item.PostId == postId, includeProperties: "Category,PostTags,PostTags.Tag");

        public PostViewModel GetViewPostByPostId(string postId) {
            var viewPost = new PostViewModel();
            var post = this.PostRepository.GetFirstOrDefault(item => item.PostId == postId, includeProperties: "Category,PostTags,PostTags.Tag");
            viewPost.Post = post;
            var (_, numberOfComment) = this.GetCommentOfPost(post);
            viewPost.NumberOfComment = numberOfComment;

            return viewPost;
        }
        public void UpdatePost(Post post) => this.PostRepository.Update(post);
        public void RemovePost(Post post) => this.PostRepository.Remove(post);
        public void AddTagToPost(Post post, Tag tag) => this.PostRepository.AddTagToPost(post, tag);
        public void RemoveTagFromPost(Post post, Tag tag) => this.PostRepository.RemoveTagFromPost(post, tag);
        public (List<Post>, int) GetPostsAndCount(int pageIndex, int pageSize, string search, string categoryId) {
            var list = this.PostRepository.GetAll(item => item.Status == PostStatus.APPROVED && (item.Title.Contains(search) || item.Student.Name.Contains(search)));

            var count = list.Count();
            var listForPage = (List<Post>) list.Take((pageIndex + 1) * pageSize).Skip(pageIndex * pageSize);

            return (listForPage, count);
        }
        public (List<Post>, int) GetPostsByCategoryWithCount(int pageSize, int pageIndex, string name) => this.PostRepository.GetPostsByCategoryWithCount(pageIndex, pageSize, name);
        public List<Tag> GetTagsFromPost(Post post) => this.PostRepository.GetTagsFromPost(post);
        public (List<Post>, int) GetPostsByTagWithCount(int pageSize, int pageIndex, string name) => this.PostRepository.GetPostsByTagWithCount(pageIndex, pageSize, name);
        public (List<Post>, int) GetPostsOfStudentWithStatus(int pageSize, int pageIndex, string studentId, PostStatus status) => this.PostRepository.GetPostsOfStudentWithStatus(pageSize, pageIndex, studentId, status);
        public (List<Post>, int) GetWaitPostsWithCount() => this.PostRepository.GetWaitPostsWithCount();
        public (List<Post>, int) GetPopularPosts(int quantity) {
            var list = (List<Post>) this.PostRepository.GetAll(options: o => o.OrderBy(p => p.View).Take(quantity).ToList(),
                                                                includeProperties: "Category,Student");
            return (list, quantity);
        }

        public (List<Post>, int) GetHighestPointPosts(int quantity) {
            var list = (List<Post>) this.PostRepository.GetAll(options: o => o.OrderBy(p => p.Like - p.Dislike + p.View / 10).Take(quantity).ToList());
            return (list, quantity);
        }

        public (List<Post>, int) GetNewestPosts(int quantity) {
            var list = (List<Post>) this.PostRepository.GetAll(options: o => o.OrderBy(p => p.CreateDate).Take(quantity).ToList(),
                                                                includeProperties: "Category,Student");
            return (list, quantity);
        }
        public int CalculatePostPoint(Post post) {
            int result = post.Like - post.Dislike + (post.View / 10);
            return result;
        }

        public void LikePost(Post post, User user) {
            this.PostRepository.LikePost(post, user);
            return;
        }
        public void DislikePost(Post post, User user) {
            this.PostRepository.DislikePost(post, user);
            return;
        }
        public (List<Comment>, int) GetCommentOfPost(Post post) {
            List<Comment> list = (List<Comment>) this.CommentRepository.GetAll(filter: item => item.PostId == post.PostId);
            int count = list.Count;

            return (list, count);
        }

        public List<string> GetPostSuggestion(string search, string categoryId) {
            var list = (IEnumerable<Post>) this.PostRepository.GetAll(item => ((int) item.Status) == 3 && (item.Title.Contains(search) || item.Student.Name.Contains(search)));
            List<string> result = list.Take(10).Skip(0).Select(item => item.Title).ToList();
            return result;
        }
    }
}
