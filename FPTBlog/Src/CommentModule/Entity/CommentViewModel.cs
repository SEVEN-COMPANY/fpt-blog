using System.Collections.Generic;

namespace FPTBlog.Src.CommentModule.Entity
{
    public class CommentViewModel
    {
        public Comment OriComment {get;set;}
        public List<Comment> SubComments{get;set;}
    }
}
