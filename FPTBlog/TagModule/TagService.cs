using FPTBlog.TagModule.Entity;
using FPTBlog.TagModule.Interface;

namespace FPTBlog.TagModule
{
    public class TagService : ITagService
    {
        private readonly ITagRepository tagRepository;
        public TagService(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        public Tag GetTagByName(string name)
        {
            return this.tagRepository.GetTagByName(name);
        }

        public Tag GetTagByTagId(string tagId)
        {
            return this.tagRepository.GetTagByTagId(tagId);
        }

        public bool SaveTag(Tag tag)
        {
            return this.tagRepository.SaveTag(tag);
        }
    }
}