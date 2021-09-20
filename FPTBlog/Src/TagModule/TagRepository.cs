using System.Collections.Generic;
using System.Linq;
using FPTBlog.Src.TagModule.Entity;
using FPTBlog.Src.TagModule.Interface;
using FPTBlog.Utils;

namespace FPTBlog.Src.TagModule
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
            List<Tag> list = this.Db.Tag.ToList();
            return list;
        }

        public Tag GetTagByName(string name)
        {
            var tag = this.Db.Tag.FirstOrDefault(item => item.Name == name);
            return tag;
        }

        public Tag GetTagByTagId(string tagId)
        {
            var tag = this.Db.Tag.FirstOrDefault(item => item.TagId == tagId);
            return tag;
        }

        public bool SaveTag(Tag tag)
        {
            this.Db.Tag.Add(tag);
            return this.Db.SaveChanges() > 0;
        }

        public bool UpdateTag(Tag tag)
        {
            Tag obj = this.Db.Tag.FirstOrDefault(item => item.TagId == tag.TagId);
            if (obj == null)
            {
                return false;
            }

            obj.Name = tag.Name;

            return this.Db.SaveChanges() > 0;
        }

        public bool DeleteTag(string tagId)
        {
            Tag obj = this.Db.Tag.FirstOrDefault(item => item.TagId == tagId);
            if (obj == null)
            {
                return false;
            }

            this.Db.Tag.Remove(obj);
            return this.Db.SaveChanges() > 0;
        }
    }
}