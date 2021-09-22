namespace FPTBlog.Utils.Common
{
    public class RouterItem
    {
        public string Title;
        public string Page;
        public string Link;
    }

    public class Routers
    {
        public static readonly RouterItem Register = new RouterItem() { Page = "/Views/Containers/Auth/Register.cshtml", Title = "Register", Link = "/auth/register" };
        public static readonly RouterItem Login = new RouterItem() { Page = "/Views/Containers/Auth/Login.cshtml", Title = "Register", Link = "/auth/login" };
        public static readonly RouterItem Home = new RouterItem() { Page = "/Views/Containers/Auth/Login.cshtml", Title = "Register", Link = "/auth/login" };
        public static readonly RouterItem User = new RouterItem() { Page = "/Views/Containers/User/Index.cshtml", Title = "Register", Link = "/auth/login" };
        public static readonly RouterItem UpdateUser = new RouterItem() { Page = "/Views/Containers/User/Update.cshtml", Title = "Update User", Link = "/user/update" };
        public static readonly RouterItem ChangePass = new RouterItem() { Page = "/Views/Containers/User/ChangePass.cshtml", Title = "Change Pass", Link = "/user/change-password" };

        // Tag
        public static readonly RouterItem AddTag = new RouterItem() { Page = "/Views/Containers/Tag/AddTag.cshtml", Title = "Add Tag", Link = "/tag/add" };
        public static readonly RouterItem UpdateTag = new RouterItem() { Page = "/Views/Containers/Tag/UpdateTag.cshtml", Title = "Update Tag", Link = "/tag/update" };
        public static readonly RouterItem GetTags = new RouterItem() { Page = "/Views/Containers/Tag/GetTags.cshtml", Title = "Get Tags", Link = "/tag" };

        // Blog
        public static readonly RouterItem GetBlog = new RouterItem() { Page = "/Views/Containers/Blog/GetBlog.cshtml", Title = "Get Blog", Link = "/blog/get" };
        public static readonly RouterItem GetAllBlogs = new RouterItem() { Page = "/Views/Containers/Blog/GetAllBlogs.cshtml", Title = "Get All Blogs", Link = "/blog/all" };
        public static readonly RouterItem EditorPage = new RouterItem() { Page = "/Views/Containers/Blog/EditorPage.cshtml", Title = "Write Your Blog", Link = "/blog" };
        public static readonly RouterItem Category = new RouterItem() { Page = "/Views/Containers/Category/Category.cshtml", Title = "All Category", Link = "/category" };
        public static readonly RouterItem CreateCategory = new RouterItem() { Page = "/Views/Containers/Category/CreateCategory.cshtml", Title = "Create Category", Link = "/category/create" };
        public static readonly RouterItem UpdateCategory = new RouterItem() { Page = "/Views/Containers/Category/UpdateCategory.cshtml", Title = "Update Category", Link = "/category/update" };
        public static readonly RouterItem DeleteCategory = new RouterItem() { Page = "/Views/Containers/Category/DeleteCategory.cshtml", Title = "Delete Category", Link = "/category/delete" };
    }
}