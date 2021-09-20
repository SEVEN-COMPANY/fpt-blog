using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FPTBlog.Src.BlogModule.Entity;

namespace FPTBlog.Src.TagModule.Entity
{
    [Table("tblTag")]
    public class Tag
    {
        [Key]
        [Required]
        [StringLength(40)]
        public string TagId { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        public string CreateDate { get; set; }

        public ICollection<BlogTag> BlogTags { get; set; }
        public Tag()
        {
            this.TagId = Guid.NewGuid().ToString();
            this.Name = "";
            this.CreateDate = DateTime.Now.ToShortDateString();
            this.BlogTags = new List<BlogTag>();
        }

        public override string ToString()
        {
            return "Tag: \n TagId: " + TagId + " \nName: " + Name
            + " \nCreateDate: " + CreateDate;
        }
    }
}