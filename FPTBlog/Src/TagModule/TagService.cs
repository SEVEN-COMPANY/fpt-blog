using System.Collections.Generic;
using FPTBlog.Src.TagModule.Entity;
using FPTBlog.Src.TagModule.Interface;
using Microsoft.AspNetCore.Mvc.Rendering;
using FPTBlog.Src.BlogModule.Entity;
namespace FPTBlog.Src.TagModule
{
    public class TagService : ITagService
    {
        private readonly ITagRepository TagRepository;
        public TagService(ITagRepository tagRepository)
        {
            this.TagRepository = tagRepository;
        }

        public List<IDictionary<string, object>> GetTagsWithCount()
        {
            var list = new List<IDictionary<string, object>>();
            var tags = this.TagRepository.GetTags();

            foreach (var item in tags)
            {
                var tagWithCount = new Dictionary<string, object>();
                tagWithCount.Add("tag", item);
                tagWithCount.Add("quantity", this.TagRepository.GetQualityBlogOfTag(item.TagId));
                list.Add(tagWithCount);
            }

            return list;

        }

        public Tag GetTagByName(string name)
        {
            return this.TagRepository.GetTagByName(name);
        }

        public Tag GetTagByTagId(string tagId)
        {
            return this.TagRepository.GetTagByTagId(tagId);
        }


        public bool SaveTag(Tag tag)
        {
            return this.TagRepository.SaveTag(tag);
        }

        public bool UpdateTag(Tag tag)
        {
            return this.TagRepository.UpdateTag(tag);
        }

        public bool DeleteTag(string tagId)
        {
            return this.TagRepository.DeleteTag(tagId);
        }

        public List<SelectListItem> GetRadioStatusList()
        {
            SelectListItem active = new SelectListItem() { Value = ((int)TagStatus.ACTIVE).ToString(), Text = "active" };
            SelectListItem inactive = new SelectListItem() { Value = ((int)TagStatus.INACTIVE).ToString(), Text = "inactive" };
            return new List<SelectListItem>() { active, inactive };
        }

        public List<SelectListItem> GetRadioCategoryList()
        {
            var tags = new List<SelectListItem>();

            var list = this.TagRepository.GetTags();
            foreach (var item in list)
            {
                tags.Add(new SelectListItem() { Value = item.TagId, Text = item.Name });
            }

            return tags;
        }
    }
}