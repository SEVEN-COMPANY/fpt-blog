using System.Collections.Generic;
using FPTBlog.Src.CategoryModule.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FPTBlog.Src.CategoryModule.Interface {
    public interface ICategoryService {
        # region Add, Update, Remove
        public void UpdateCategory(Category category);
        public void AddCategory(Category category);
        #endregion
        public (List<Category>, int) GetCategories();
        public Category GetCategoryByName(string name);
        public Category GetCategoryByCategoryId(string categoryId);

        # region Get For Page
        public (List<Category>, int) GetCategoriesAndCount(int pageIndex, int pageSize, string searchName, CategoryStatus searchStatus);
        public List<SelectListItem> GetCategoryStatusDropList();
        public List<SelectListItem> GetCategoryDropList();
        public List<CategoryChart> GetCategoryChart();
        # endregion
    }
}
