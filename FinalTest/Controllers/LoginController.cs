//Created or Uptaded by Roxane Seibel / student number : 74527
using FinalTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalTest.Controllers
{
    public class LoginController : Controller
    {

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Authorise(User user)
        {
            using (DBModel db = new DBModel())
            {
                var userDetail = db.User.Where(x => x.Name == user.Name && x.LastName == user.LastName && x.AccountNumber == user.AccountNumber && x.Passeword == user.Passeword).FirstOrDefault();
                if (userDetail == null)
                {
                    user.LoginErrorMessage = "Wrong Identification or Passeword";
                    return View("Index", user);
                }
                else
                {
                    Session["id"] = userDetail.Id;
                    Session["accountNumber"] = userDetail.AccountNumber;
                    Session["balance"] = userDetail.Balance;
                    Session["email"] = userDetail.Email;
                    Session["name"] = userDetail.Name;
                    Session["lastName"] = userDetail.LastName;
                    Session["accountType"] = userDetail.AccountType;
                    return RedirectToAction("Index", "Customer",userDetail);
                }
            }

        }

        public ActionResult LogOut()
        {
            int userId = (int)Session["userID"];
            Session.Abandon();
            return RedirectToAction("Index", "Menu");
        }
    }
}