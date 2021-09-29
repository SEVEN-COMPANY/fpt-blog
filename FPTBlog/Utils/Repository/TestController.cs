using System.Collections.Generic;
using FPTBlog.Src.BlogModule.Entity;
using FPTBlog.Src.CategoryModule.Entity;
using FPTBlog.Utils.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FPTBlog.Utils.Repository
{
    [Route("/api/test")]
    public class TestController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public TestController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("category")]
        public IActionResult GetCategory(string categoryId){
            Category category = _unitOfWork.Category.Get(categoryId);
            List<Category> listCategoryBelongToBlog = (List<Category>)_unitOfWork.Category.GetAll();

            return Json(new{
                category = category,
                list = listCategoryBelongToBlog
            });
        }

        [HttpGet("blog")]
        public IActionResult GetBlog(string blogId){
            Blog blog = _unitOfWork.Blog.Get(blogId);
            List<Blog> getBlogByCategory = (List<Blog>)_unitOfWork.Blog.GetAll(item => item.CategoryId == "2866e4d4-4138-4ee7-9087-2d2215d0aaa2");
            List<Blog> getBlogByStudent = (List<Blog>)_unitOfWork.Blog.GetAll(item => item.StudentId == "c810e062-28ca-4877-873c-a178dd568955");

            return Json(new{
                blog = blog,
                getBlogByCategory = getBlogByCategory,
                getBlogByStudent = getBlogByStudent
            });
        }
    }
}
