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
        public static readonly RouterItem AddTag = new RouterItem() { Page = "/Views/Containers/Tag/AddTag.cshtml", Title = "Add Tag", Link = "/tag/add" };
        public static readonly RouterItem UpdateTag = new RouterItem() { Page = "/Views/Containers/Tag/UpdateTag.cshtml", Title = "Update Tag", Link = "/tag/update" };
        public static readonly RouterItem GetTags = new RouterItem() { Page = "/Views/Containers/Tag/GetTags.cshtml", Title = "Get Tags", Link = "/tag" };
        public static readonly RouterItem Home = new RouterItem() { Page = "/Views/Containers/Auth/Login.cshtml", Title = "Register", Link = "/auth/login" };
        public static readonly RouterItem User = new RouterItem() { Page = "/Views/Containers/User/Index.cshtml", Title = "Register", Link = "/auth/login" };
        public static readonly RouterItem UpdateUser = new RouterItem() { Page = "/Views/Containers/User/Update.cshtml", Title = "Update User", Link = "/user/update" };
        public static readonly RouterItem Category = new RouterItem() { Page = "/Views/Containers/Category/Category.cshtml", Title = "All Category", Link = "/category" };
        public static readonly RouterItem CreateCategory = new RouterItem() { Page = "/Views/Containers/Category/CreateCategory.cshtml", Title = "Create Category", Link = "/category/create" };
        public static readonly RouterItem UpdateCategory = new RouterItem() { Page = "/Views/Containers/Category/UpdateCategory.cshtml", Title = "Update Category", Link = "/category/update" };
    }
}