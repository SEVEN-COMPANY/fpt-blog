using System.Collections.Generic;
using FPTBlog.Src.TagModule.Entity;


namespace FPTBlog.Src.TagModule.Interface {
    public interface ITagRepository {
        public bool SaveTag(Tag tag);
        public Tag GetTagByTagId(string tagId);
        public Tag GetTagByName(string name);
        public bool UpdateTag(Tag tag);
        public List<Tag> GetTags();
        public (List<Tag>, int) GetTagsWithFilter(int pageIndex, int pageSize, string searchName, TagStatus searchStatus);
        public bool DeleteTag(string tagId);
        public int GetQualityBlogOfTag(string tagId);

        public List<Tag> GetTagsByName(string name);
    }
}
