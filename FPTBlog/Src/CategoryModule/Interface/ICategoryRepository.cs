using System.Collections.Generic;
using FPTBlog.Src.CategoryModule.Entity;

namespace FPTBlog.Src.CategoryModule.Interface {
    public interface ICategoryRepository {
        public Category GetCategoryByCategoryName(string name);
        public Category GetCategoryByCategoryId(string categoryId);
        public List<Category> GetCategories(int currentPage, int pageSize, string name);
        public bool SaveCategory(Category category);
        public List<Category> GetCategories();
        public bool UpdateCategory(Category category);
        public bool DeleteCategory(Category category);
    }
}
