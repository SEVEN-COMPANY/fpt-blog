
using System.Collections.Generic;
using System.Linq;
using FPTBlog.Src.TagModule.Entity;
using FPTBlog.Src.TagModule.Interface;
using FPTBlog.Utils;
using FPTBlog.Src.PostModule.Entity;
using FPTBlog.Utils.Repository;
using System;

namespace FPTBlog.Src.TagModule {
    public class TagRepository : Repository<Tag>, ITagRepository {
        private readonly DB Db;
        public TagRepository(DB dB) : base(dB) {
            this.Db = dB;
        }

        public (List<Tag>, int) GetTagsWithCount(int pageIndex, int pageSize, string searchName, TagStatus searchStatus) {
            var list = (IEnumerable<Tag>) this.GetAll(item => item.Name.Contains(searchName) && item.Status == searchStatus);
            var count = list.Count();
            var pagelist = list.Take((pageIndex + 1) * pageSize).Skip(pageIndex * pageSize).ToList();

            return (pagelist, count);
        }

        public int NumberOfPostBelongToTag(string tagId) {
            List<PostTag> listBlogByTag = (from PostTag in this.Db.PostTag
                                           where tagId.CompareTo(PostTag.TagId) == 0
                                           select PostTag).ToList();
            return listBlogByTag.Count();
        }

        public (int, int) GetCreatedTag() {
            int createdTagThisMonth = 0;
            int createdTagLastMonth = 0;
            string thisMonth = DateTime.Now.AddMonths(-1).ToShortDateString();
            DateTime thisMonthDate = Convert.ToDateTime(thisMonth);
            string lastMonth = DateTime.Now.AddMonths(-2).ToShortDateString();
            DateTime lastMonthDate = Convert.ToDateTime(lastMonth);

            List<Tag> tags = this.Db.Tag.ToList();

            foreach (var tag in tags) {
                DateTime date = Convert.ToDateTime(tag.CreateDate);
                if (DateTime.Compare(date, thisMonthDate) > 0) {
                    createdTagThisMonth++;
                }
                if (DateTime.Compare(date, lastMonthDate) > 0 && (DateTime.Compare(date, thisMonthDate) < 0)) {
                    createdTagLastMonth++;
                }
            }
            return (createdTagLastMonth, createdTagThisMonth);
        }

        public (string, int) GetHotTrendingTag() {
            var listTagDescending = this.Db.PostTag.AsEnumerable()
                                        .GroupBy(p => p.TagId)
                                        .OrderByDescending(p => p.Count())
                                        .Select(p => new{ TagId = p.Key, Count = p.Count()})
                                        .ToList();
            string hotTrendingTagId = listTagDescending[0].TagId;
            Tag hotTrendingTag = this.Db.Tag.FirstOrDefault(item => item.TagId == hotTrendingTagId);

            int count = listTagDescending[0].Count;

            return (hotTrendingTag.Name, count);
        }

        public List<string> GetUsedTagIds() => this.Db.PostTag.Select(item => item.TagId).ToList();

    }
}
