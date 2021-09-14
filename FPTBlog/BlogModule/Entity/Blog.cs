using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FPTBlog.BlogModule.Entity
{
    [Table("tblBlog")]
    public class Blog
    {
        [Key]
        [Required]
        [StringLength(40)]
        public string BlogId {get;set;}

        [Required]
        [StringLength(40)]
        public string Title {get;set;}

        [Required]
        public string Content {get;set;}

        [Required]
        [StringLength(255)]
        public string Description {get;set;}

        [Required]
        [Range(1, 60)]
        public int ReadTime {get;set;}

        [Required]
        [Range(1, 60)]
        public int Like {get;set;}

        [Required]
        [Range(1, 60)]
        public int Dislike {get;set;}

        [Required]
        public BlogStatus status {get;set;}

        [Required]
        [StringLength(20)]
        public string CreateDate { get; set; }

    }

    public enum BlogStatus {
        DRAFT = 0,
        WAIT = 1,
        APPROVED = 2,
        DENY = 3
    }
}