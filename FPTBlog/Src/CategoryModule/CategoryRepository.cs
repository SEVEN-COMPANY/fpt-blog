using FPTBlog.Src.CategoryModule.Entity;
using FPTBlog.Src.CategoryModule.Interface;
using FPTBlog.Utils;
using System.Linq;
using System.Collections.Generic;
using System;

namespace FPTBlog.Src.CategoryModule {
    public class CategoryRepository : ICategoryRepository {
        private readonly DB DB;

        public CategoryRepository(DB dB) {
            this.DB = dB;
        }

        public (List<Category>, int) GetCategoriesAndCount(int pageIndex, int pageSize, string searchName, CategoryStatus searchStatus) {
            var query = (from Category in this.DB.Category where Category.Name.Contains(searchName) && Category.Status == searchStatus select Category);
            int count = query.Count();
            List<Category> categories = query.Take((pageIndex + 1) * pageSize).Skip(pageIndex * pageSize).ToList();


            return (categories, count);
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
