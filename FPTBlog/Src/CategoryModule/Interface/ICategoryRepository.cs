using System.Collections.Generic;
using FPTBlog.Src.CategoryModule.Entity;

namespace FPTBlog.Src.CategoryModule.Interface
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