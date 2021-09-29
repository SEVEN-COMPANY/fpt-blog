namespace FPTBlog.Utils.Common {
    public class RouterItem {
        public string Title;
        public string Page;
        public string Link;
    }

    public class Routers {
        // Auth

        public static readonly RouterItem AuthPostRegister = new RouterItem() { Page = "/Views/Containers/Auth/Register.cshtml", Title = "Register", Link = "/auth/register" };
        public static readonly RouterItem AuthPostLogin = new RouterItem() { Page = "/Views/Containers/Auth/Login.cshtml", Title = "Login", Link = "/auth/login" };
        public static readonly RouterItem AuthGetLogout = new RouterItem() { Page = "/Views/Containers/Auth/Login.cshtml", Title = "Login", Link = "/auth/logout" };

        // Post
        public static readonly RouterItem PostGetDraftList = new RouterItem() { Page = "/Views/Containers/Post/DraftList.cshtml", Title = "My Draft", Link = "/post/me" };
        public static readonly RouterItem PostGetPreview = new RouterItem() { Page = "/Views/Containers/Post/Preview.cshtml", Title = "", Link = "/post/preview" };

        public static readonly RouterItem PostGetEditor = new RouterItem() { Page = "/Views/Containers/Post/Editor.cshtml", Title = "Write Your Blog", Link = "/post/editor" };
        public static readonly RouterItem PostGetPost = new RouterItem() { Page = "/Views/Containers/Post/Post.cshtml", Title = "Write Your Blog", Link = "/post" };

        // User
        public static readonly RouterItem CommonGetHome = new RouterItem() { Page = "/Views/Containers/Auth/Login.cshtml", Title = "Home", Link = "/home" };
        public static readonly RouterItem UserGetProfile = new RouterItem() { Page = "/Views/Containers/User/Profile.cshtml", Title = "User", Link = "/user" };
        public static readonly RouterItem UserPutUser = new RouterItem() { Page = "/Views/Containers/User/Update.cshtml", Title = "Update User", Link = "/user/update" };

        public static readonly RouterItem UserPutPassword = new RouterItem() { Page = "/Views/Containers/User/PutPassword.cshtml", Title = "Change Pass", Link = "/user/change-password" };

    }
}
