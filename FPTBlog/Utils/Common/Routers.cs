namespace FPTBlog.Utils.Common {
    public class RouterItem {
        public string Title {
            get; set;
        }
        public string Page {
            get; set;
        }
        public string Link {
            get; set;
        }
    }

    public class Routers {

        public static readonly RouterItem ServerURL = new RouterItem() { Page = "", Title = "", Link = "https://fptblog.vinhnhan.com" };


        // Home
        public static readonly RouterItem CommonGetHome = new RouterItem() { Page = "/Views/Containers/Home/Home.cshtml", Title = "Home", Link = "/" };

        // Auth
        public static readonly RouterItem AuthPostRegister = new RouterItem() { Page = "/Views/Containers/Auth/Register.cshtml", Title = "Register", Link = "/auth/register" };
        public static readonly RouterItem AuthPostLogin = new RouterItem() { Page = "/Views/Containers/Auth/Login.cshtml", Title = "Login", Link = "/auth/login" };
        public static readonly RouterItem AuthGetLogout = new RouterItem() { Page = "/Views/Containers/Auth/Login.cshtml", Title = "Logout", Link = "/auth/logout" };

        // Post
        public static readonly RouterItem PostGetDraftList = new RouterItem() { Page = "/Views/Containers/Post/DraftList.cshtml", Title = "My Draft", Link = "/post/me" };
        public static readonly RouterItem PostGetSearch = new RouterItem() { Page = "/Views/Containers/Post/Search.cshtml", Title = "Search Post", Link = "/post/search" };
        public static readonly RouterItem PostGetPreview = new RouterItem() { Page = "/Views/Containers/Post/Preview.cshtml", Title = "Review", Link = "/post/preview" };

        public static readonly RouterItem PostGetEditor = new RouterItem() { Page = "/Views/Containers/Post/Editor.cshtml", Title = "Write Your Post", Link = "/post/editor" };
        public static readonly RouterItem PostGetPost = new RouterItem() { Page = "/Views/Containers/Post/Post.cshtml", Title = "Write Your Post", Link = "/post" };

        // User

        //Comment
        public static readonly RouterItem AddComment = new RouterItem() { Page = "/Views/Containers/Comment/AddComment.cshtml", Link = "/comment/add" };
        public static readonly RouterItem GetComment = new RouterItem() { Page = "/Views/Containers/Comment/GetComment.cshtml", Link = "/comment/get" };
        public static readonly RouterItem UpdateComment = new RouterItem() { Page = "/Views/Containers/Comment/UpdateComment.cshtml", Link = "comment/update" };
        public static readonly RouterItem UserGetProfile = new RouterItem() { Page = "/Views/Containers/User/Profile.cshtml", Title = "User", Link = "/user/profile" };
        public static readonly RouterItem UserGetMyProfile = new RouterItem() { Page = "/Views/Containers/User/MyProfile.cshtml", Title = "User", Link = "/user/me" };


        public static readonly RouterItem UserGetFollower = new RouterItem() { Page = "/Views/Containers/User/Follower.cshtml", Title = "User", Link = "/user/follower" };
        public static readonly RouterItem UserGetFollowing = new RouterItem() { Page = "/Views/Containers/User/Following.cshtml", Title = "User", Link = "/user/following" };


        public static readonly RouterItem UserPutUser = new RouterItem() { Page = "/Views/Containers/User/UpdateProfile.cshtml", Title = "Update User", Link = "/user/update" };

        public static readonly RouterItem UserPutPassword = new RouterItem() { Page = "/Views/Containers/User/ChangePassword.cshtml", Title = "Change Password", Link = "/user/change-password" };


        // Notification
        public static readonly RouterItem NotificationMe = new RouterItem() { Page = "/Views/Containers/Notification/Notification.cshtml", Title = "My Notification", Link = "/notification/me" };

        //Common
        public static readonly RouterItem NotFoundPage = new RouterItem() { Page = "/Views/Containers/Error/404.cshtml", Title = "", Link = "" };
        public static readonly RouterItem ErrorPage = new RouterItem() { Page = "/Views/Containers/Error/500.cshtml", Title = "", Link = "" };
    }
}
