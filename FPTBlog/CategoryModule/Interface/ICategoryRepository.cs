using System.Collections.Generic;
using FPTBlog.CategoryModule.Entity;

namespace FPTBlog.CategoryModule.Interface
{
    public interface ICategoryRepository
    {
        public Category GetCategoryByCategoryName(string name);
        public Category GetCategoryByCategoryId(string categoryId);
        public List<Category> GetCategories();
        public bool SaveCategory(Category category);
        public bool UpdateCategory(Category category);
        public bool DeleteCategory(Category category);
    }
}