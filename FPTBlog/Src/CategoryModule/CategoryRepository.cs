using FPTBlog.Src.CategoryModule.Entity;
using FPTBlog.Src.CategoryModule.Interface;
using FPTBlog.Utils;
using System.Linq;
using System.Collections.Generic;

namespace FPTBlog.Src.CategoryModule {
    public class CategoryRepository : ICategoryRepository {
        private readonly DB DB;

        public CategoryRepository(DB dB) {
            this.DB = dB;
        }

        public List<Category> GetCategories(int currentPage, int pageSize, string name) {
            List<Category> categories = this.DB.Category.Where(item => item.Name.Contains(name)).Take((pageSize + 1) * currentPage).Skip(currentPage * pageSize).ToList();
            return categories;
        }
        public List<Category> GetCategories() {
            List<Category> categories = this.DB.Category.ToList();
            return categories;
        }

        public Category GetCategoryByCategoryName(string name) {
            Category category = this.DB.Category.FirstOrDefault(item => item.Name == name);
            return category;
        }
        public Category GetCategoryByCategoryId(string categoryId) {
            Category category = this.DB.Category.FirstOrDefault(item => item.CategoryId == categoryId);
            return category;
        }
        public bool SaveCategory(Category category) {
            this.DB.Category.Add(category);
            return this.DB.SaveChanges() > 0;
        }

        public bool UpdateCategory(Category category) {
            Category obj = this.GetCategoryByCategoryId(category.CategoryId);

            obj.Name = category.Name;
            obj.Description = category.Description;
            obj.Status = category.Status;

            return this.DB.SaveChanges() > 0;
        }

        public bool DeleteCategory(Category category) {
            Category obj = this.GetCategoryByCategoryId(category.CategoryId);

            obj.Status = 0;

            return this.DB.SaveChanges() > 0;
        }
    }
}
