//Created or Uptaded by Roxane Seibel / student number : 74527

using FinalTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalTest.Controllers
{
    public class BankController : Controller
    {
        // GET: Bank
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Validation(User user)
        {
            using (DBModel db = new DBModel())
            {
                user.Name = "Bank";
                var userDetail = db.User.Where(x => x.Name == "Bank" && x.Passeword == user.Passeword).FirstOrDefault();
                if (userDetail == null)
                {
                    user.LoginErrorMessage = "Wrong Passeword";
                    return View("Index", user);
                }
                else
                {
                    Session["userID"] = user.Id;
                    Session["userName"] = user.Name;
                    return RedirectToAction("Display", "Home");
                }
            }
        }

        public ActionResult LogOut()
        {
            return RedirectToAction("Index", "Menu");
        }
    }
}