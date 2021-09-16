using System.Collections.Generic;
using FPTBlog.TagModule.Entity;

namespace FPTBlog.TagModule.Interface
{
    public interface ITagService
    {
        public bool SaveTag(Tag tag);
        public Tag GetTagByTagId(string tagId); 
        public Tag GetTagByName(string name); 
        public bool UpdateTag(Tag tag);
        public List<Tag> GetTags();
        public bool DeleteTag(string tagId);
    }
}