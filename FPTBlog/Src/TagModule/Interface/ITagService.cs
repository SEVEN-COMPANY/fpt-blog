using System.Collections.Generic;
using FPTBlog.Src.TagModule.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FPTBlog.Src.TagModule.Interface {
    public interface ITagService {
        public (List<Tag>, int) GetTags();
        public void AddTag(Tag tag);
        public Tag GetTagByTagId(string tagId);
        public (List<Tag>, int) GetTagsByName(string name);
        public Tag GetTagByName(string name);
        public void UpdateTag(Tag tag);
        public void RemoveTag(Tag tag);
        public (List<IDictionary<string, object>>, int) GetTagsBelongToPostWithCount(int pageIndex, int pageSize, string searchName, TagStatus searchStatus);
        public List<SelectListItem> GetTagStatusDroplist();
        public (int,int) GetCreatedTag();
        public (string, int) GetHotTrendingTag();

    }
}
