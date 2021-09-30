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
        public void UpdateTag(Tag tag) => this.TagRepository.Update(tag);
        public void RemoveTag(Tag tag) => this.TagRepository.Remove(tag);
        public (List<Tag>, int) GetTagsWithCount(int pageIndex, int pageSize, string searchName, TagStatus searchStatus){
            var list = (IEnumerable<Tag>) this.TagRepository.GetAll(item => item.Name.Equals(searchName) && item.Status == searchStatus,
                                                                    includeProperties: "PostTag");
            var count = list.Count();

            var pagelist = list.Take((pageIndex + 1) * pageSize).Skip(pageIndex * pageSize).ToList();

            return (pagelist, count);
        }

        public List<SelectListItem> GetRadioStatusList() {
            SelectListItem active = new SelectListItem() { Value = ((int) TagStatus.ACTIVE).ToString(), Text = "active" };
            SelectListItem inactive = new SelectListItem() { Value = ((int) TagStatus.INACTIVE).ToString(), Text = "inactive" };
            return new List<SelectListItem>() { active, inactive };
        }

    }
}
