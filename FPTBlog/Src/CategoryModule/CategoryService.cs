using System.Collections.Generic;
using FPTBlog.Src.CategoryModule.Interface;
using FPTBlog.Src.CategoryModule.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FPTBlog.Src.CategoryModule {
    public class CategoryService : ICategoryService {
        private readonly ICategoryRepository CategoryRepository;

        public CategoryService(ICategoryRepository categoryRepository) {

            this.CategoryRepository = categoryRepository;
        }

        public List<Category> GetCategories() {
            return this.CategoryRepository.GetCategories();
        }

        public bool SaveCategory(Category category) {
            return this.CategoryRepository.SaveCategory(category);
        }

        public Category GetCategoryByCategoryName(string name) {
            return this.CategoryRepository.GetCategoryByCategoryName(name);
        }

        public Category GetCategoryByCategoryId(string categoryId) {
            return this.CategoryRepository.GetCategoryByCategoryId(categoryId);
        }

        public bool UpdateCategory(Category category) {
            return this.CategoryRepository.UpdateCategory(category);
        }

        public bool DeleteCategory(Category category) {
            return this.CategoryRepository.DeleteCategory(category);
        }


        public List<SelectListItem> GetRadioStatusList() {
            SelectListItem active = new SelectListItem() { Value = ((int) CategoryStatus.ACTIVE).ToString(), Text = "active" };
            SelectListItem inactive = new SelectListItem() { Value = ((int) CategoryStatus.INACTIVE).ToString(), Text = "inactive" };
            return new List<SelectListItem>() { active, inactive };
        }

        public List<SelectListItem> GetCategoryDropList() {
            var categories = new List<SelectListItem>();

            var list = this.CategoryRepository.GetCategories();
            foreach (var item in list) {
                categories.Add(new SelectListItem() { Value = item.CategoryId, Text = item.Name });
            }

            return categories;
        }
    }
}
