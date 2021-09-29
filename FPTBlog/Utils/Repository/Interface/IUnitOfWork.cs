namespace FPTBlog.Utils.Repository.Interface
{
    public interface IUnitOfWork
    {
        public void SaveChange();
        public INewCategoryRepository Category {get;}
        public INewBlogRepository Blog {get;}
    }
}
