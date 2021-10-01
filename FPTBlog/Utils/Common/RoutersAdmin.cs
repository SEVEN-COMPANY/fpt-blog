

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



        //user
        public static readonly RouterAdminItem UserPutUser = new RouterAdminItem() { Page = "/ViewsAdmin/Containers/User/Update.cshtml", Link = "/admin/user/update" };
        public static readonly RouterAdminItem UserPutPassword = new RouterAdminItem() { Page = "/ViewsAdmin/Containers/User/ChangePassword.cshtml", Link = "/admin/user/change-password" };
        public static readonly RouterAdminItem UserGetUserList = new RouterAdminItem() { Page = "/ViewsAdmin/Containers/Admin/ListUser.cshtml", Link = "/admin/user/list" };
        // Category
        public static readonly RouterAdminItem CategoryGetCategoryList = new RouterAdminItem() { Page = "/ViewsAdmin/Containers/Category/List.cshtml", Link = "/admin/category" };
        public static readonly RouterAdminItem CategoryPost = new RouterAdminItem() { Page = "/ViewsAdmin/Containers/Category/CreateCategory.cshtml", Link = "/admin/category/create" };
        public static readonly RouterAdminItem CategoryPutCategory = new RouterAdminItem() { Page = "/ViewsAdmin/Containers/Category/Update.cshtml", Link = "/admin/category/update" };


        // Tag
        public static readonly RouterAdminItem TagGetTagList = new RouterAdminItem() { Page = "/ViewsAdmin/Containers/Tag/List.cshtml", Link = "/admin/tag" };
    }
}
