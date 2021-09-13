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
        public static readonly RouterItem CreateBlog = new RouterItem() { Page = "/Views/Containers/Blog/CreateBlog.cshtml", Title = "Register", Link = "/auth/login" };
    }
}