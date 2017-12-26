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
    public class ArticleRepository:IRepository<Article>
    {
        //implementation IRepository which typed Article
        private BlogContext db;

       public ArticleRepository()
       {
            this.db = new BlogContext();
       }
        public void Create(Article item)
        {
            db.Articles.Add(item);
        }

        public void Delete(int id)
        {
            Article article = db.Articles.Find(id);
            if (article != null)
                db.Articles.Remove(article);
        }

        public Article GetById(int id)
        {
            return db.Articles.Find(id);
        }

        public IEnumerable<Article> GetList()
        {
            return db.Articles;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Article item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
