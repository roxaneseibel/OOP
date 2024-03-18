//Created or Uptaded by Roxane Seibel / student number : 74527
using FinalTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

namespace FinalTest.Controllers
{
    public class HomeController : Controller
    {
        DBModel db = new DBModel();
        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(User ut)
        {
            db.User.Add(ut);
            db.SaveChanges();
            return RedirectToAction("Display");
        }

        [HttpGet]
        public ActionResult Display()
        {
            var obj = db.User.ToList();
            return View(obj);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var obj = db.User.Find(id);
            return View(obj);
        }

        [HttpPost]
        public ActionResult Edit(User ut)
        {
            if (ut.Balance >= 0 && ut.AccountType >= 0)
            {

                db.Entry(ut).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Display");
            }
            else
            {
                ut.LoginErrorMessage = "You cannot have negative balance";
                return View("Edit", ut);
            }

        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            var obj = db.User.Find(id);
            return View(obj);
        }

        [HttpPost]
        public ActionResult Delete(User ut)
        {
            var userDetail = db.User.Where(x => x.Id == ut.Id).FirstOrDefault();
            if (userDetail.Balance == 0 && userDetail.AccountType == 0)
            {

                db.Entry(userDetail).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("Display");
            }
            else
            {
                userDetail.LoginErrorMessage = "The balance is not at 0";
                return View("Delete", userDetail);
            }
        }

        public ActionResult Details(int id)
        {
            var obj = db.User.Find(id);
            return View(obj);
        }

        public ActionResult Back()
        {
            return RedirectToAction("Display", "Home");
        }

        [HttpGet]
        public ActionResult Transaction(int id)
        {
            var obj = db.User.Find(id);
            return View(obj);
        }

        [HttpPost]
        public ActionResult Transaction(User ut)
        {
            if (ut.Balance >= 0 && ut.AccountType >= 0)
            {

                db.Entry(ut).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Display");
            }
            else
            {
                ut.LoginErrorMessage = "You cannot have negative balance";
                return View("Transaction", ut);
            }


            
        }
    }
    
}