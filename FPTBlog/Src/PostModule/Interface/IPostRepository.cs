using System.Collections.Generic;
using FPTBlog.Src.BlogModule.Entity;
using FPTBlog.Src.TagModule.Entity;
using FPTBlog.Src.UserModule.Entity;
using FPTBlog.Utils.Repository.Interface;

namespace FPTBlog.Src.BlogModule.Interface {
    public interface IPostRepository : IRepository<Post> {

        public List<Tag> GetTagsFromPost(Post blog);
        public void RemoveTagFromPost(Post blog, Tag tag);
        public void AddTagToPost(Post blog, Tag tag);
        public (List<Post>, int) GetPostsByTagWithCount(int pageSize, int pageIndex, string name);
        public (List<Post>, int) GetBlogsByCategoryWithCount(int pageSize, int pageIndex, string name);

        public (List<Post>, int) GetBlogsOfStudentWithStatus(int pageSize, int pageIndex, string studentId, PostStatus status);
        public void LikeBlog(Post blog, User user);
        public (List<Post>, int) GetWaitBlogAndCount();
    }
}
