
using System.Collections.Generic;
using System.Linq;
using FPTBlog.Src.TagModule.Entity;
using FPTBlog.Src.TagModule.Interface;
using FPTBlog.Utils;
using FPTBlog.Src.BlogModule.Entity;
using FPTBlog.Utils.Repository;
namespace FPTBlog.Src.TagModule {
    public class TagRepository : Repository<Tag>, ITagRepository {
        private readonly DB Db;
        public TagRepository(DB dB) : base(dB) {
            this.Db = dB;
        }

        public (List<Tag>, int) GetTagsWithCount(int pageIndex, int pageSize, string searchName, TagStatus searchStatus) {
            var list = (IEnumerable<Tag>) this.GetAll(item => item.Name.Equals(searchName) && item.Status == searchStatus);
            var count = list.Count();

            foreach (Tag item in list) {
                item.PostCount = this.NumberOfPostBelongToTag(item.TagId);
            }

            var pagelist = (List<Tag>) this.GetEntityByPage(list, pageSize, pageIndex);

            return (pagelist, count);
        }

        private int NumberOfPostBelongToTag(string tagId) {
            List<PostTag> listBlogByTag = (from PostTag in this.Db.PostTag
                                           where tagId.CompareTo(PostTag.TagId) == 0
                                           select PostTag).ToList();
            return listBlogByTag.Count();
        }
    }
}
