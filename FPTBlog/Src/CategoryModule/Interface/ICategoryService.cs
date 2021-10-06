using System.Collections.Generic;
using FPTBlog.Src.CategoryModule.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FPTBlog.Src.CategoryModule.Interface {
    public interface ICategoryService {
        public (List<Category>, int) GetCategories();
        public void AddCategory(Category category);
        public Category GetCategoryByName(string name);
        public Category GetCategoryByCategoryId(string categoryId);
        public void UpdateCategory(Category category);
        public void RemoveCategory(Category category);
        public (List<Category>, int) GetCategoriesAndCount(int pageIndex, int pageSize, string searchName, CategoryStatus searchStatus);
        public List<SelectListItem> GetCategoryStatusDropList();
        public List<SelectListItem> GetCategoryDropList();
    }
}
