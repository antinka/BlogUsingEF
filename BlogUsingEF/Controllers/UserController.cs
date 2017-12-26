using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogUsingEF.DAL.Repositories;
using BlogUsingEF.DAL;
using BlogUsingEF.DAL.Entities;
using BlogUsingEF.DAL.Interfaces;

namespace BlogUsingEF.Controllers
{
    public class UserController : Controller
    {
        IRepository<User> db;
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }  
        // add new user where userName unic
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(User user)
        {
            db = new UserRepository();
            if (ModelState.IsValid)
            {
                //check for exist such userName
                var ifExistUserName = (from b in db.GetList() where b.UserName == user.UserName select b).Count();
                if (ifExistUserName == 0)
                {
                    db.Create(user);
                    db.Save();
                    var details = db.GetList().Single(u => u.UserName == user.UserName && u.Password == user.Password);
                    Session["UserId"] = details.Id.ToString();
                    Session["UserName"] = details.UserName.ToString();
                    return RedirectToAction("Welcom");
                }
                ModelState.AddModelError("", "this login exist");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            db = new UserRepository();
            // search user which try to login and put id and userName in relevant session
            try
            {
                
                var details = db.GetList().Single(u => u.UserName == user.UserName && u.Password == user.Password);
                if (details != null)
                {
                    Session["UserId"] = details.Id.ToString();
                    Session["UserName"] = details.UserName.ToString();
                    return RedirectToAction("Welcom");
                }
            }
            // if such user didnt find
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error");
            }
            return View(user);

        }
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Welcom");
        }
        public ActionResult Welcom()
        {
            return View();
        }
    }
}