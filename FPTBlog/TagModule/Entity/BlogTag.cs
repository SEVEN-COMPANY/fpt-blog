using System.ComponentModel.DataAnnotations.Schema;
using FPTBlog.BlogModule.Entity;

namespace FPTBlog.TagModule.Entity
{
    [Table("tblBlogTag")]
    public class BlogTag
    {
        public string BlogId {get;set;}
        
        public virtual Blog Blog {get;set;}

        public string TagId {get;set;}
        
        public virtual Tag Tag {get;set;}
    }
}