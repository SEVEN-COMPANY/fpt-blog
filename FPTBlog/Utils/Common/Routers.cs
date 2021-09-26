namespace FPTBlog.Utils.Common {
    public class RouterItem {
        public string Title;
        public string Page;
        public string Link;
    }

    public class Routers {
        // Auth

        public static readonly RouterItem Register = new RouterItem() { Page = "/Views/Containers/Auth/Register.cshtml", Title = "Register", Link = "/auth/register" };
        public static readonly RouterItem Login = new RouterItem() { Page = "/Views/Containers/Auth/Login.cshtml", Title = "Login", Link = "/auth/login" };

        // User
        public static readonly RouterItem Home = new RouterItem() { Page = "/Views/Containers/Auth/Login.cshtml", Title = "Home", Link = "/auth/login" };
        public static readonly RouterItem User = new RouterItem() { Page = "/Views/Containers/User/User.cshtml", Title = "User", Link = "/user" };
        public static readonly RouterItem UpdateUser = new RouterItem() { Page = "/Views/Containers/User/Update.cshtml", Title = "Update User", Link = "/user/update" };

        public static readonly RouterItem ChangePass = new RouterItem() { Page = "/Views/Containers/User/ChangePass.cshtml", Title = "Change Pass", Link = "/user/change-password" };

        // Admin
        public static readonly RouterItem AdminDashboard = new RouterItem() { Page = "/Views/Containers/Admin/Dashboard.cshtml", Title = "Change Pass", Link = "/admin" };


        // Blog
        public static readonly RouterItem GetBlog = new RouterItem() { Page = "/Views/Containers/Blog/GetBlog.cshtml", Title = "Get Blog", Link = "/blog/get" };
        public static readonly RouterItem GetAllBlogs = new RouterItem() { Page = "/Views/Containers/Blog/GetAllBlogs.cshtml", Title = "Get All Blogs", Link = "/blog/all" };
        public static readonly RouterItem EditorPage = new RouterItem() { Page = "/Views/Containers/Blog/EditorPage.cshtml", Title = "Write Your Blog", Link = "/blog" };



    }
}
