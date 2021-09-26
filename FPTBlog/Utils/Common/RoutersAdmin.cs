using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPTBlog.Utils.Common {
    public class RouterAdminItem {
        public string Page;
        public string Link;
    }

    public class RoutersAdmin {

        //user
        public static readonly RouterAdminItem UpdateUser = new RouterAdminItem() { Page = "/ViewsAdmin/Containers/User/Update.cshtml", Link = "/admin/user/update" };
        public static readonly RouterAdminItem ChangePassword = new RouterAdminItem() { Page = "/ViewsAdmin/Containers/User/ChangePassword.cshtml", Link = "/admin/user/change-password" };
        // Category
        public static readonly RouterAdminItem Category = new RouterAdminItem() { Page = "/ViewsAdmin/Containers/Category/List.cshtml", Link = "/admin/category" };
        public static readonly RouterAdminItem CreateCategory = new RouterAdminItem() { Page = "/ViewsAdmin/Containers/Category/CreateCategory.cshtml", Link = "/admin/category/create" };
        public static readonly RouterAdminItem UpdateCategory = new RouterAdminItem() { Page = "/ViewsAdmin/Containers/Category/Update.cshtml", Link = "/admin/category/update" };

    }
}
