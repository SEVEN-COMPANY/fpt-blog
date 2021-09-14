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
        
        // Tag
        public static readonly RouterItem AddTag = new RouterItem() {Page = "/Views/Containers/Tag/AddTag.cshtml", Title = "Add Tag", Link = "/tag/add"};
        public static readonly RouterItem UpdateTag = new RouterItem() {Page = "/Views/Containers/Tag/UpdateTag.cshtml", Title = "Update Tag", Link = "/tag/update"};
        public static readonly RouterItem GetTags = new RouterItem() {Page = "/Views/Containers/Tag/GetTags.cshtml", Title = "Get Tags", Link = "/tag"};
    
        // Blog
        public static readonly RouterItem AddBlog = new RouterItem() {Page = "/Views/Containers/Blog/AddBlog.cshtml", Title = "Add Blog", Link = "/blog/add"};
        public static readonly RouterItem GetBlog = new RouterItem() {Page = "/Views/Containers/Blog/GetBlog.cshtml", Title = "Get Blog", Link = "/blog"};
        public static readonly RouterItem GetAllBlogs = new RouterItem() {Page = "/Views/Containers/Blog/GetAllBlogs.cshtml", Title = "Get All Blogs", Link = "/blog/all"};
    }
}