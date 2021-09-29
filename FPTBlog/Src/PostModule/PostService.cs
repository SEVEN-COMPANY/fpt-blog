using System.Collections.Generic;
using FPTBlog.Src.BlogModule.Entity;
using FPTBlog.Src.BlogModule.Interface;
using FPTBlog.Src.TagModule.Entity;
using FPTBlog.Src.UserModule.Entity;

namespace FPTBlog.Src.BlogModule {
    public class PostService : IBlogService {
        private readonly IPostRepository PostRepository;
        public PostService(IPostRepository blogRepository) {
            this.PostRepository = blogRepository;
        }

        public void AddTagToBlog(Post blog, Tag tag) {
            this.PostRepository.AddTagToPost(blog, tag);
        }

        public Post GetBlogByBlogId(string postId) => this.PostRepository.GetFirstOrDefault(filter: item => item.PostId.Equals(postId), includeProperties: "Student,Category");


        public List<Tag> GetTagsFromBlog(Post blog) {
            return this.PostRepository.GetTagsFromPost(blog);
        }

        public void RemoveTagFromBlog(Post blog, Tag tag) {
            this.PostRepository.RemoveTagFromPost(blog, tag);
        }

        public void AddBlog(Post blog) => this.PostRepository.Add(blog);
        public void UpdateBlog(Post blog) => this.PostRepository.Update(blog);



        public (List<Post>, int) GetBlogsByTagAndCount(int pageSize, int pageIndex, string name) {
            return this.PostRepository.GetPostsByTagWithCount(pageSize, pageIndex, name);
        }
        public (List<Post>, int) GetAllBlogsAndCount(int pageSize, int pageIndex) {
            return this.PostRepository.GetPostsWithCount(pageSize, pageIndex);
        }
        public (List<Post>, int) GetBlogsByCategoryAndCount(int pageSize, int pageIndex, string name) {
            return this.PostRepository.GetBlogsByCategoryWithCount(pageSize, pageIndex, name);
        }

        public (List<Post>, int) GetBlogsOfStudentWithStatus(int pageSize, int pageIndex, string studentId, PostStatus status) {
            return this.PostRepository.GetBlogsOfStudentWithStatus(pageSize, pageIndex, studentId, status);
        }

        public void LikeBlog(Post blog, User user) => this.PostRepository.LikeBlog(blog, user);


        public (List<Post>, int) GetBlogsWaitWithCount() => this.PostRepository.GetWaitBlogAndCount();

        public int CalculateBlogPoint(Post blog) {
            int result = blog.Like - blog.Dislike + (blog.View / 10);
            return result;
        }

    }
}
