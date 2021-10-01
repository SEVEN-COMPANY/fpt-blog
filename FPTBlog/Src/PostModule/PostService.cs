using System.Collections.Generic;
using System.Linq;
using FPTBlog.Src.PostModule.Entity;
using FPTBlog.Src.PostModule.Interface;
using FPTBlog.Src.TagModule.Entity;
using FPTBlog.Src.UserModule.Entity;

namespace FPTBlog.Src.PostModule {
    public class PostService : IPostService {
        private readonly IPostRepository PostRepository;
        public PostService(IPostRepository postRepository) {
            this.PostRepository = postRepository;
        }

        public void AddPost(Post post) => this.PostRepository.Add(post);
        public Post GetPostByPostId(string postId) => this.PostRepository.GetFirstOrDefault(item => item.PostId == postId, includeProperties: "Category,PostTags,PostTags.Tag");
        public void UpdatePost(Post post) => this.PostRepository.Update(post);
        public void RemovePost(Post post) => this.PostRepository.Remove(post);
        public void AddTagToPost(Post post, Tag tag) => this.PostRepository.AddTagToPost(post, tag);
        public void RemoveTagFromPost(Post post, Tag tag) => this.PostRepository.RemoveTagFromPost(post, tag);
        public (List<Post>, int) GetPostsAndCount(int pageIndex, int pageSize, PostStatus searchStatus) {
            List<Post> list = (List<Post>) this.PostRepository.GetAll(item => item.Status == searchStatus);
            var count = list.Count;
            list = (List<Post>) list.Take((pageIndex + 1) * pageSize).Skip(pageIndex * pageSize);

            return (list, count);
        }
        public (List<Post>, int) GetPostsByCategoryWithCount(int pageSize, int pageIndex, string name) => this.PostRepository.GetPostsByCategoryWithCount(pageIndex, pageSize, name);
        public List<Tag> GetTagsFromPost(Post post) => this.PostRepository.GetTagsFromPost(post);
        public (List<Post>, int) GetPostsByTagWithCount(int pageSize, int pageIndex, string name) => this.PostRepository.GetPostsByTagWithCount(pageIndex, pageSize, name);
        public (List<Post>, int) GetPostsOfStudentWithStatus(int pageSize, int pageIndex, string studentId, PostStatus status) => this.PostRepository.GetPostsOfStudentWithStatus(pageSize, pageIndex, studentId, status);
        public (List<Post>, int) GetWaitPostsWithCount() => this.PostRepository.GetWaitPostsWithCount();
        public int CalculatePostPoint(Post post) {
            int result = post.Like - post.Dislike + (post.View / 10);
            return result;
        }
    }
}
