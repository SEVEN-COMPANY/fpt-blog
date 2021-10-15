using System.Collections.Generic;
using FPTBlog.Src.CategoryModule.Interface;
using FPTBlog.Src.CategoryModule.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System;
using FPTBlog.Src.PostModule.Interface;
using FPTBlog.Src.PostModule.Entity;

namespace FPTBlog.Src.CategoryModule {
    public class CategoryService : ICategoryService {
        private readonly ICategoryRepository CategoryRepository;
        private readonly IPostRepository PostRepository;

        public CategoryService(ICategoryRepository categoryRepository, IPostRepository postRepository) {

            this.CategoryRepository = categoryRepository;
            this.PostRepository = postRepository;
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
        public (List<Category>, int) GetAllCategories(int pageIndex, int pageSize, string searchName) => this.CategoryRepository.GetAllCategories(pageIndex, pageSize, searchName);

        public List<SelectListItem> GetCategoryStatusDropList() {
            var status = new List<SelectListItem>(){
                new SelectListItem(){ Value = CategoryStatus.ACTIVE.ToString(), Text = "Active"},
                new SelectListItem(){  Value =  CategoryStatus.INACTIVE.ToString(), Text = "Inactive"}
            };

            return status;
        }

        public List<SelectListItem> GetCategoryDropList() {
            var categories = new List<SelectListItem>();

            var list = this.CategoryRepository.GetAll();
            foreach (var item in list) {
                categories.Add(new SelectListItem() { Value = item.CategoryId, Text = item.Name });
            }

            return categories;
        }

        public List<CategoryChart> GetCategoryChart() {
            string thisMonth = DateTime.Now.AddMonths(-1).ToShortDateString();
            DateTime thisMonthDate = Convert.ToDateTime(thisMonth);
            List<CategoryChart> chart = new List<CategoryChart>();
            var categories = this.CategoryRepository.GetAll();
            foreach (var category in categories) {
                var categoryChart = new CategoryChart();
                categoryChart.name = category.Name;
                int total = 0;
                List<Post> posts = this.PostRepository.GetAll(item => item.CategoryId == category.CategoryId).ToList();
                for (int i = posts.Count - 1; i >= 0; i--) {
                    DateTime date = Convert.ToDateTime(posts[i].CreateDate);
                    if (DateTime.Compare(date, thisMonthDate) < 0) {
                        posts.Remove(posts[i]);
                    }
                    else {
                        total += 1;
                    }
                }
                categoryChart.total = total;
                chart.Add(categoryChart);

            }
            return chart;
        }
    }
}
