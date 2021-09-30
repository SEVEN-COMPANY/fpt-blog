using System.Collections.Generic;
using FPTBlog.Src.CategoryModule.Interface;
using FPTBlog.Src.CategoryModule.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace FPTBlog.Src.CategoryModule {
    public class CategoryService : ICategoryService {
        private readonly ICategoryRepository CategoryRepository;

        public CategoryService(ICategoryRepository categoryRepository) {

            this.CategoryRepository = categoryRepository;
        }
        public (List<Category>, int) GetCategories() {
            List<Category> list = (List<Category>) this.CategoryRepository.GetAll();
            var count = list.Count;
            return (list, count);
        }
        public void AddCategory(Category category) => this.CategoryRepository.Add(category);
        public Category GetCategoryByCategoryId(string categoryId) => this.CategoryRepository.Get(categoryId);
        public Category GetCategoryByName(string name) => this.CategoryRepository.GetFirstOrDefault(item => item.Name.Equals(name));
        public void UpdateCategory(Category category) => this.CategoryRepository.Update(category);
        public void RemoveCategory(Category category) => this.CategoryRepository.Remove(category);
        public (List<Category>, int) GetCategoriesAndCount(int pageIndex, int pageSize, string searchName, CategoryStatus searchStatus) => this.CategoryRepository.GetCategoriesAndCount(pageIndex, pageSize, searchName, searchStatus);

        public List<SelectListItem> GetRadioStatusList() {
            SelectListItem active = new SelectListItem() { Value = ((int) CategoryStatus.ACTIVE).ToString(), Text = "active" };
            SelectListItem inactive = new SelectListItem() { Value = ((int) CategoryStatus.INACTIVE).ToString(), Text = "inactive" };
            return new List<SelectListItem>() { active, inactive };
        }

        public List<SelectListItem> GetCategoryDropList() {
            var categories = new List<SelectListItem>();

            var list = this.CategoryRepository.GetAll();
            foreach (var item in list) {
                categories.Add(new SelectListItem() { Value = item.CategoryId, Text = item.Name });
            }

            return categories;
        }
    }
}
