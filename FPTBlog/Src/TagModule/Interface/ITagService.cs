using System.Collections.Generic;
using FPTBlog.Src.TagModule.Entity;
using FPTBlog.Src.BlogModule.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FPTBlog.Src.TagModule.Interface
{
    public interface ITagService
    {
        public bool SaveTag(Tag tag);
        public Tag GetTagByTagId(string tagId);
        public Tag GetTagByName(string name);
        public bool UpdateTag(Tag tag);
        public List<IDictionary<string, object>> GetTagsWithCount();
        public bool DeleteTag(string tagId);
        public List<SelectListItem> GetRadioStatusList();
        public List<SelectListItem> GetRadioCategoryList();

    }
}