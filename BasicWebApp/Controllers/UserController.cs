using BasicWebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BasicWebApp.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        //send data
        [HttpPost]
        public ActionResult Authorize(LoginDetail userModel)
        {


            if (ModelState.IsValid)
            {
                using (WebAppLogin db = new WebAppLogin())
                {
                    var userDetails = db.LoginDetails.Where(x => x.Username == userModel.Username && x.Password == userModel.Password && x.Userrole == userModel.Userrole).FirstOrDefault();
                    if (userDetails == null)
                    {
                       // userModel.ErrorMessage = "Wrong username or password";
                        ViewBag.Error = "Wrong credentials";
                        return View("Login");
                    }
                    else
                    {
                        // custom form authentication
                        FormsAuthentication.SetAuthCookie(userDetails.Username, false);
                        //store state in session
                        Session["uname"] = userDetails.Username.ToString();
                         if ((userDetails.Userrole !=null) )
                        {

                            return RedirectToAction("Details", "Home");
                        }

                    }
                }

            }

            return View("Login");


        }

        public ActionResult Logout()
        {
            //clear session
            Session.Clear();
            FormsAuthentication.SignOut();
            // if session is not cleared, it is set to null
            Session["uname"] = null;
            return RedirectToAction("Login", "User");
        }
    }
}