using FPTBlog.Src.CategoryModule.Entity;
using FPTBlog.Src.CategoryModule.Interface;
using FPTBlog.Utils;
using System.Linq;
using System.Collections.Generic;
using FPTBlog.Utils.Repository;

namespace FPTBlog.Src.CategoryModule {
    public class CategoryRepository : Repository<Category>, ICategoryRepository {
        private readonly DB DB;

        public CategoryRepository(DB dB) : base(dB) {
            this.DB = dB;
        }
    }
}
