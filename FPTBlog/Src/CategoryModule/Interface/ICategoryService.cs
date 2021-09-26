using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using FPTBlog.Src.CategoryModule.DTO;
using FPTBlog.Src.CategoryModule.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FPTBlog.Src.CategoryModule.Interface {
    public interface ICategoryService {
        public List<Category> GetCategories();
        public bool SaveCategory(Category category);
        public Category GetCategoryByCategoryName(string name);
        public Category GetCategoryByCategoryId(string categoryId);
        public bool UpdateCategory(Category category);
        public bool DeleteCategory(Category category);
        public List<SelectListItem> GetRadioStatusList();
        public List<SelectListItem> GetRadioCategoryList();
    }
}
