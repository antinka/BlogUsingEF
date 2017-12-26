using BlogUsingEF.DAL;
using BlogUsingEF.DAL.Entities;
using BlogUsingEF.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogUsingEF.DAL.Repositories
{
   public class CommentRepository : IRepository<Comment>
    {
        //implementation IRepository which typed Comment
        private BlogContext db;

        public CommentRepository()
        {
            this.db = new BlogContext();
        }

        public void Create(Comment item)
        {
            db.Comments.Add(item);
        }

        public void Delete(int id)
        {
            Comment comment = db.Comments.Find(id);
            if (comment != null)
                db.Comments.Remove(comment);
        }

        public Comment GetById(int id)
        {
            return db.Comments.Find(id);
        }

        public IEnumerable<Comment> GetList()
        {
            return db.Comments;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Comment item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
