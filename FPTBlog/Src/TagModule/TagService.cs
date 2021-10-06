using System;
using System.Linq;
using System.Collections.Generic;
using FPTBlog.Src.TagModule.Entity;
using FPTBlog.Src.TagModule.Interface;
using FPTBlog.Utils.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FPTBlog.Src.TagModule {
    public class TagService : ITagService {
        private readonly ITagRepository TagRepository;
        public TagService(ITagRepository tagRepository) {
            this.TagRepository = tagRepository;
        }
        public (List<Tag>, int) GetTags() {
            List<Tag> list = (List<Tag>) this.TagRepository.GetAll(includeProperties: "PostTags");
            var count = list.Count;
            return (list, count);
        }

        public void AddTag(Tag tag) => this.TagRepository.Add(tag);
        public Tag GetTagByTagId(string tagId) => this.TagRepository.Get(tagId);
        public Tag GetTagByName(string name) => this.TagRepository.GetFirstOrDefault(item => item.Name == name);

        public (List<Tag>, int) GetTagsByName(string name) {
            var list = (List<Tag>) this.TagRepository.GetAll(item => item.Name.Contains(name));

            return (list, list.Count());
        }

        public void UpdateTag(Tag tag) => this.TagRepository.Update(tag);
        public void RemoveTag(Tag tag) => this.TagRepository.Remove(tag);
        public (List<Tag>, int) GetTagsWithCount(int pageIndex, int pageSize, string searchName, TagStatus searchStatus) {
            var list = (IEnumerable<Tag>) this.TagRepository.GetAll(item => item.Name.Equals(searchName) && item.Status == searchStatus,
                                                                    includeProperties: "PostTag");
            var count = list.Count();

            var pagelist = list.Take((pageIndex + 1) * pageSize).Skip(pageIndex * pageSize).ToList();

            return (pagelist, count);
        }
        public (List<IDictionary<string, object>>, int) GetTagsBelongToPostWithCount(int pageIndex, int pageSize, string searchName, TagStatus searchStatus) {
            var list = new List<IDictionary<string, object>>();
            var (tags, count) = this.TagRepository.GetTagsWithCount(pageIndex, pageSize, searchName, searchStatus);
            foreach (Tag item in tags) {
                var tagWithCount = new Dictionary<string, object>();
                tagWithCount.Add("tag", item);
                tagWithCount.Add("quantity", this.TagRepository.NumberOfPostBelongToTag(item.TagId));
                list.Add(tagWithCount);
            }

            return (list, count);
        }

        public List<SelectListItem> GetTagStatusDroplist() {
            SelectListItem active = new SelectListItem() { Value = ((int) TagStatus.ACTIVE).ToString(), Text = "Active" };
            SelectListItem inactive = new SelectListItem() { Value = ((int) TagStatus.INACTIVE).ToString(), Text = "Inactive" };
            return new List<SelectListItem>() { active, inactive };
        }

    }
}
