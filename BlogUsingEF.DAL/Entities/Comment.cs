using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogUsingEF.DAL.Entities
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public DateTime DataPublish { get; set; }
        public string Text { get; set; }
        public Article Article { get; set; }
        public int? ArticleId { get; set; }
        public User User { get; set; }
        public int? UserId { get; set; }
    }
}