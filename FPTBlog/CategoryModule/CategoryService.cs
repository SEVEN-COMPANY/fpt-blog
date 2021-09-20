using System.Collections.Generic;
using System;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using FPTBlog.CategoryModule.Interface;
using FPTBlog.CategoryModule.Entity;
using FPTBlog.CategoryModule.DTO;
using FPTBlog.Utils.Common;
using FPTBlog.Utils.Locale;
using FPTBlog.Utils;
using System.Linq;

namespace FPTBlog.CategoryModule
{
    public class CategoryService : ICategoryService
    {
        private readonly DB DB;
        private readonly ICategoryRepository CategoryRepository;

        public CategoryService(DB dB, ICategoryRepository categoryRepository)
        {
            this.DB = dB;
            this.CategoryRepository = categoryRepository;
        }

        public List<Category> GetCategories()
        {
            return this.CategoryRepository.GetCategories();
        }

        public bool SaveCategory(Category category)
        {
            return this.CategoryRepository.SaveCategory(category);
        }

        public Category GetCategoryByCategoryName(string name)
        {
            return this.CategoryRepository.GetCategoryByCategoryName(name);
        }

        public Category GetCategoryByCategoryId(string categoryId)
        {
            return this.CategoryRepository.GetCategoryByCategoryId(categoryId);
        }

        public bool UpdateCategory(Category category)
        {
            return this.CategoryRepository.UpdateCategory(category);
        }

        public bool DeleteCategory(Category category)
        {
            return this.CategoryRepository.DeleteCategory(category);
        }
    }
}