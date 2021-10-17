using System.Collections.Generic;
using FPTBlog.Src.PostModule.Entity;
using FPTBlog.Src.TagModule.Entity;
using FPTBlog.Src.UserModule.Entity;
using FPTBlog.Utils.Repository.Interface;

namespace FPTBlog.Src.PostModule.Interface {
    public interface IPostRepository : IRepository<Post> {

        public void AddTagToPost(Post blog, Tag tag);
        public void RemoveTagFromPost(Post blog, Tag tag);
        public (List<Post>, int) GetPostsByCategoryWithCount(int pageSize, int pageIndex, string name);
        public List<Tag> GetTagsFromPost(Post blog);
        public (List<Post>, int) GetPostsByTagWithCount(int pageSize, int pageIndex, string name);

        public (List<Post>, int) GetPostsOfStudentWithStatus(int pageSize, int pageIndex, string studentId);
        public (List<Post>, int) GetWaitPostsWithCount();
        public void LikePost(Post post, User user);
        public void DislikePost(Post post, User user);
        public Report GetMonthlyReport();
        public (List<Post>, int) GetPostsByStatus(int pageSize, int pageIndex, string search, PostStatus status);
        public (List<Post>, int) GetAllPosts(int pageSize, int pageIndex, string search);
        public List<PostChart> GetPostChart();
    }
}
