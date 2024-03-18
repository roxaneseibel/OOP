
//Created or Uptaded by Roxane Seibel / student number : 74527
using FinalTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalTest.Controllers
{
    public class CustomerController : Controller
    {
        private DBModel db = new DBModel();
        // GET: Customer
        public ActionResult Index(User user)
        {
            
            var obj = db.User.Find(user.Id);
            return View(obj);
            
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
                return RedirectToAction("Index", ut);
            }
            else
            {
                ut.LoginErrorMessage = "You cannot have negative balance";
                return View("Transaction", ut);
            }
        }

        public ActionResult Back(User ut)
        {
            return RedirectToAction("Index", "Customer",ut);
        }
    }
}