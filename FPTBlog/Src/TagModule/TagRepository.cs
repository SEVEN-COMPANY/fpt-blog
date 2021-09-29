
using System.Collections.Generic;
using System.Linq;
using FPTBlog.Src.TagModule.Entity;
using FPTBlog.Src.TagModule.Interface;
using FPTBlog.Utils;
using FPTBlog.Src.BlogModule.Entity;

namespace FPTBlog.Src.TagModule {
    public class TagRepository : ITagRepository {
        private readonly DB Db;
        public TagRepository(DB Db) {
            this.Db = Db;
        }

        public List<Tag> GetTags() {
            List<Tag> list = this.Db.Tag.ToList();
            return list;
        }
        public List<Tag> GetTagsByName(string name) {
            List<Tag> list = this.Db.Tag.Where(item => item.Name.Contains(name)).ToList();
            return list;
        }

        public int GetQualityBlogOfTag(string tagId) {
            List<BlogTag> listBlogByTag = (from BlogTag in this.Db.BlogTag
                                           where tagId.CompareTo(BlogTag.TagId) == 0
                                           select BlogTag).ToList();
            return listBlogByTag.Count();
        }

        public Tag GetTagByName(string name) {
            var tag = this.Db.Tag.FirstOrDefault(item => item.Name == name);
            return tag;
        }

        public Tag GetTagByTagId(string tagId) {
            var tag = this.Db.Tag.Find(tagId);
            return tag;
        }


        public bool SaveTag(Tag tag) {
            this.Db.Tag.Add(tag);
            return this.Db.SaveChanges() > 0;
        }

        public bool UpdateTag(Tag tag) {
            Tag obj = this.Db.Tag.FirstOrDefault(item => item.TagId == tag.TagId);
            if (obj == null) {
                return false;
            }

            obj.Name = tag.Name;

            return this.Db.SaveChanges() > 0;
        }

        public bool DeleteTag(string tagId) {
            Tag obj = this.Db.Tag.FirstOrDefault(item => item.TagId == tagId);
            if (obj == null) {
                return false;
            }

            this.Db.Tag.Remove(obj);
            return this.Db.SaveChanges() > 0;
        }

        public (List<Tag>, int) GetTagsWithFilter(int pageIndex, int pageSize, string searchName, TagStatus searchStatus) {
            var query = (from Tag in this.Db.Tag
                         where Tag.Name.Contains(searchName) && Tag.Status == searchStatus
                         select Tag);
            List<Tag> tags = query.Take((pageIndex + 1) * pageSize).Skip(pageIndex * pageSize).ToList();
            return (tags, tags.Count);
        }
    }
}
