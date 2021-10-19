using System.Collections.Generic;
using FPTBlog.Src.CategoryModule.Entity;
using FPTBlog.Utils.Repository.Interface;

namespace FPTBlog.Src.CategoryModule.Interface {
    public interface ICategoryRepository : IRepository<Category> {
        (List<Category>, int) GetCategoriesAndCount(int pageIndex, int pageSize, string searchName, CategoryStatus searchStatus);
    }
}
