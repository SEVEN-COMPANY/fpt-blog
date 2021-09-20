using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FPTBlog.Src.TagModule.Entity;

namespace FPTBlog.Src.BlogModule.Entity
{
    [Table("tblBlogTag")]
    public class BlogTag
    {
        public string BlogId { get; set; }

        public virtual Blog Blog { get; set; }

        public string TagId { get; set; }

        public virtual Tag Tag { get; set; }
    }
}