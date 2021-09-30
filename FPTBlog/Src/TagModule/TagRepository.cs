
using System.Collections.Generic;
using System.Linq;
using FPTBlog.Src.TagModule.Entity;
using FPTBlog.Src.TagModule.Interface;
using FPTBlog.Utils;
using FPTBlog.Src.PostModule.Entity;
using FPTBlog.Utils.Repository;
namespace FPTBlog.Src.TagModule {
    public class TagRepository : Repository<Tag>, ITagRepository {
        private readonly DB Db;
        public TagRepository(DB dB) : base(dB) {
            this.Db = dB;
        }
    }
}
