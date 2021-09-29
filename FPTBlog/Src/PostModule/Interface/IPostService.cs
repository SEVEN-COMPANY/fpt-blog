using System.Collections.Generic;
using FPTBlog.Src.BlogModule.Entity;
using FPTBlog.Src.TagModule.Entity;
using FPTBlog.Src.UserModule.Entity;

namespace FPTBlog.Src.BlogModule.Interface {
    public interface IBlogService {
        public (List<Post>, int) GetBlogsWaitWithCount();
    }
}
