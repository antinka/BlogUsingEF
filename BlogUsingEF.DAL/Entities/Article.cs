using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogUsingEF.DAL.Entities
{
    public class Article
    { 
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DataPublish { get; set; }
        public string Text { get; set; }
        public User User { get; set; }
        public int? UserId { get; set; }
        public IList<Comment> Comments { get; set; }
    }
}