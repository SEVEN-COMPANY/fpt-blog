using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FPTBlog.Src.CommentModule.Entity;
using FPTBlog.Src.CommentModule.Interface;
using FPTBlog.Src.PostModule.Entity;
using FPTBlog.Src.PostModule.Interface;
using FPTBlog.Src.TagModule.Entity;
using FPTBlog.Src.UserModule.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FPTBlog.Src.PostModule {
    public class PostService : IPostService {
        private readonly IPostRepository PostRepository;
        private readonly ICommentRepository CommentRepository;
        public PostService(IPostRepository postRepository, ICommentRepository commentRepository) {
            this.PostRepository = postRepository;
            this.CommentRepository = commentRepository;
        }

        public void AddPost(Post post) => this.PostRepository.Add(post);
        public Post GetPostByPostId(string postId) => this.PostRepository.GetFirstOrDefault(item => item.PostId == postId, includeProperties: "Category,PostTags,PostTags.Tag,Student");
        public PostViewModel GetViewPostByPostId(string postId) {
            var viewPost = new PostViewModel();
            var post = this.PostRepository.GetFirstOrDefault(item => item.PostId == postId, includeProperties: "Category,PostTags,PostTags.Tag,Student");
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
            var list = this.PostRepository.GetAll(item => ((item.Title.Contains(search) || item.Student.Name.Contains(search)) && item.CategoryId.Contains(categoryId)), includeProperties: "Category,Student");
            var count = list.Count();
            var listForPage = (List<Post>) list.Take((pageIndex + 1) * pageSize).Skip(pageIndex * pageSize).ToList();

            return (listForPage, count);
        }

        public List<Tag> GetTagsFromPost(Post post) => this.PostRepository.GetTagsFromPost(post);
        public (List<PostViewModel>, int) GetPostsByTagWithCount(int pageSize, int pageIndex, string name) {
            var (posts, count) = this.PostRepository.GetPostsByTagWithCount(pageSize, pageIndex, name);
            List<PostViewModel> postViewModels = new List<PostViewModel>();
            foreach (var post in posts) {
                PostViewModel postViewModel = new PostViewModel();
                var (_, numberOfComment) = this.GetCommentOfPost(post);
                postViewModel.Post = post;
                postViewModel.NumberOfComment = numberOfComment;
                postViewModels.Add(postViewModel);
            }
            return (postViewModels, count);
        }
        public (List<Post>, int) GetPostsOfStudentWithStatusForPage(int pageSize, int pageIndex, string studentId, PostStatus status) => this.PostRepository.GetPostsOfStudentWithStatus(pageSize, pageIndex, studentId, status);

        public (List<Post>, int) GetPopularPosts(int quantity) {
            var list = (List<Post>) this.PostRepository.GetAll(options: o => o.OrderBy(p => p.View).Take(quantity).ToList(), includeProperties: "Category,Student");
            return (list, quantity);
        }
        public (List<Post>, int) GetHighestPointPosts(int quantity) {
            var list = (List<Post>) this.PostRepository.GetAll(options: o => o.OrderBy(p => p.Like - p.Dislike + p.View / 10).Take(quantity).ToList(), includeProperties: "Category,Student");
            return (list, quantity);
        }
        public (List<Post>, int) GetNewestPosts(int quantity) {
            var list = (List<Post>) this.PostRepository.GetAll(options: o => o.OrderBy(p => p.CreateDate).Take(quantity).ToList(), includeProperties: "Category,Student");
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
        public (List<Post>, int) GetPostsForProfile(int pageSize, int pageIndex, string searchTitle, string searchCategoryId, PostStatus status) {
            Expression<Func<Post, bool>> filter = null;

            if (searchCategoryId == string.Empty) {
                filter = item => item.Status == PostStatus.APPROVED && item.Title.Contains(searchTitle);
            }
            else {
                filter = item => item.Status == PostStatus.APPROVED && item.Title.Contains(searchTitle) && item.CategoryId == searchCategoryId;
            }
            // var list = this.PostRepository.GetAll(filter: filter, includeProperties: "Category");
            var list = this.PostRepository.GetAll(filter = item => item.Status == status && item.Title.Contains(searchTitle));
            int count = list.Count();
            var listForView = (List<Post>) list.Take((pageIndex + 1) * pageSize).Skip(pageIndex * pageSize).ToList();
            return (listForView, count);
        }

        public (List<Post>, int) GetPostsOfStudentWithStatus(string userId, PostStatus status) {
            var posts = this.PostRepository.GetAll(item => item.StudentId == userId && item.Status == status).ToList();
            int count = posts.Count;
            return (posts, count);
        }
        public Report GetMonthlyReport() {
            return this.PostRepository.GetMonthlyReport();
        }

        public (List<Post>, int) GetPostsByStatus(int pageSize, int pageIndex, string search, PostStatus status) {
            return this.PostRepository.GetPostsByStatus(pageSize, pageIndex, search, status);

        }

        public (List<Post>, int) GetAllPosts(int pageSize, int pageIndex, string search) {
            return this.PostRepository.GetAllPosts(pageSize, pageIndex, search);

        }

        public List<SelectListItem> GetPostStatusDropList() {
            var status = new List<SelectListItem>(){
                new SelectListItem(){ Value = PostStatus.APPROVED.ToString(), Text = "Approved"},
                new SelectListItem(){  Value =  PostStatus.DENY.ToString(), Text = "Denied"},
                new SelectListItem(){  Value =  PostStatus.WAIT.ToString(), Text = "Waiting"}
            };

            return status;
        }

    }
}
