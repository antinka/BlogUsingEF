using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BlogUsingEF.DAL.Repositories;
using BlogUsingEF.DAL;
using  BlogUsingEF.DAL.Entities;
using BlogUsingEF.DAL.Interfaces;
namespace BlogUsingEF.Controllers
{
    public class HomeController : Controller
    {
        // make variable for working with db
        IRepository<Article> dbArticle=new ArticleRepository();
        IRepository<Comment> dbComment = new CommentRepository();
        IRepository<User> dbUser= new UserRepository(); 

        public ActionResult Index()
        {
            //return all article
            return View( dbArticle.GetList());
        }
        public ActionResult ViewMyArrticle()
        {
            //return all article which own login user and present for him delete it
            return View( dbArticle.GetList().Where(u => u.UserId == Int32.Parse((string)(Session["UserId"]))));
        }
        [HttpGet]
        public ActionResult AddArticle()
        {
            return View();
        }
        //add article where userId take from session data
        [HttpPost]
        public ActionResult AddArticle(Article article)
        {
            Article art = new Article();
            art.Text = article.Text;
            art.Title = article.Title;
            art.DataPublish = DateTime.Now;
            string idUser = ((string)(Session["UserId"]));
            art.UserId = Int32.Parse(idUser);
            dbArticle.Create(art);
            dbArticle.Save();
            return RedirectToAction("Index");
        }
        // delete the article and all comment which it has
        public ActionResult DeleteArticle(int id)
        {
           Article article = dbArticle.GetById(id);
            if(article!=null)
            {   dbArticle.Delete(id);
                var IdDeleteComment = (from c in dbComment.GetList() where c.ArticleId == id select c.Id).ToList();
                for (int i = 0; i < IdDeleteComment.Count; i++)
                {
                    dbComment.Delete(IdDeleteComment[i]);
                }
                dbComment.Save();
                dbArticle.Save();
                
            }
            return RedirectToAction("Index");
        }
     //return all comment which has the article 
        public ActionResult ViewComment(int id)
        {
            return View(dbComment.GetList().Where(u => u.ArticleId==id));
        }
        //add comment to choosen article 
        [HttpGet]
        public ActionResult AddComment(int id)
        {
            ViewBag.id = id;
            return View();
        }
        [HttpPost]
        public ActionResult AddComment(Comment coment,string IdArticle)
        {
            coment.DataPublish = DateTime.Now;
            string idUser = ((string)(Session["UserId"]));
            coment.UserId = Int32.Parse(idUser);
            coment.ArticleId = Int32.Parse(IdArticle);
            dbComment.Create(coment);
            dbComment.Save();
            return RedirectToAction("Index");

        }



    }
}