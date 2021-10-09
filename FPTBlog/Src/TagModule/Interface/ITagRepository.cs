using System.Collections.Generic;
using FPTBlog.Src.TagModule.Entity;
using FPTBlog.Utils.Repository.Interface;

namespace FPTBlog.Src.TagModule.Interface {
    public interface ITagRepository : IRepository<Tag> {
        public (List<Tag>, int) GetTagsWithCount(int pageIndex, int pageSize, string searchName, TagStatus searchStatus);
        public int NumberOfPostBelongToTag(string tagId);
        public (int,int) GetCreatedTag();
        public (string, int) GetHotTrendingTag();
        public List<string> GetUsedTagIds();

    }
}
