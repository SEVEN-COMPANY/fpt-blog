using System.Collections.Generic;
using System.Linq;
using FPTBlog.TagModule.Entity;
using FPTBlog.TagModule.Interface;
using FPTBlog.Utils;

namespace FPTBlog.TagModule
{
    public class TagRepository : ITagRepository
    {
        private readonly DB Db;
        public TagRepository(DB Db)
        {
            this.Db = Db;
        }

        public List<Tag> GetTags()
        {
            List<Tag> list = this.Db.tag.ToList();
            return list;
        }

        public Tag GetTagByName(string name)
        {
            var tag = this.Db.tag.FirstOrDefault(item => item.Name == name);
            return tag;
        }

        public Tag GetTagByTagId(string tagId)
        {
            var tag = this.Db.tag.FirstOrDefault(item => item.TagId == tagId);
            return tag;
        }

        public bool SaveTag(Tag tag)
        {
            this.Db.tag.Add(tag);
            return this.Db.SaveChanges() > 0;
        }

        public bool UpdateTag(Tag tag)
        {
            Tag obj = this.Db.tag.FirstOrDefault(item => item.TagId == tag.TagId);
            if(obj == null){
                return false;
            }

            obj.Name = tag.Name;

            return this.Db.SaveChanges() > 0;
        }
    }
}