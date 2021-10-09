

namespace FPTBlog.Utils.Common {
    public class RouterAdminItem {
        public string Page {
            get; set;
        }
        public string Link {
            get; set;
        }
    }

    public class RoutersAdmin {


        // Reward
        public static readonly RouterItem RewardGetHome = new RouterItem() { Page = "/ViewsAdmin/Containers/Reward/UserList.cshtml", Link = "/" };


        //user
        public static readonly RouterAdminItem UserProfile = new RouterAdminItem() { Page = "/ViewsAdmin/Containers/User/UserProfile.cshtml", Link = "/admin/user/profile" };
        public static readonly RouterAdminItem UserPutUser = new RouterAdminItem() { Page = "/ViewsAdmin/Containers/User/Update.cshtml", Link = "/admin/user/update" };
        public static readonly RouterAdminItem UserPutPassword = new RouterAdminItem() { Page = "/ViewsAdmin/Containers/User/ChangePassword.cshtml", Link = "/admin/user/change-password" };
        public static readonly RouterAdminItem UserGetUserList = new RouterAdminItem() { Page = "/ViewsAdmin/Containers/Admin/ListUser.cshtml", Link = "/admin/user/list" };
        // Category
        public static readonly RouterAdminItem CategoryGetCategoryList = new RouterAdminItem() { Page = "/ViewsAdmin/Containers/Category/List.cshtml", Link = "/admin/category" };

        public static readonly RouterAdminItem CategoryPutCategory = new RouterAdminItem() { Page = "/ViewsAdmin/Containers/Category/Update.cshtml", Link = "/admin/category/update" };
        // Post
        public static readonly RouterAdminItem PostGetList = new RouterAdminItem() { Page = "/ViewsAdmin/Containers/Post/List.cshtml", Link = "/admin/post" };
        public static readonly RouterAdminItem PostGetPostByTag = new RouterAdminItem() { Page = "/ViewsAdmin/Containers/Post/TagPost.cshtml", Link = "/admin/post/tag" };

        // Tag
        public static readonly RouterAdminItem TagGetTagList = new RouterAdminItem() { Page = "/ViewsAdmin/Containers/Tag/List.cshtml", Link = "/admin/tag" };
    }
}
