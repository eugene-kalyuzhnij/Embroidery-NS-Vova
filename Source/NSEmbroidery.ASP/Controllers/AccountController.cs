using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using NSEmbroidery.ASP.Filters;
using NSEmbroidery.Data.Models;
using Ninject;
using NSEmbroidery.Data.Interfaces;
using NSEmbroidery.Data.DI.EF;
using NSEmbroidery.ASP.Attributes;

namespace NSEmbroidery.ASP.Controllers
{

    [Authorize]
    public class AccountController : Controller
    {
        [NoCache]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (WebSecurity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {

            if (ModelState.IsValid && WebSecurity.Login(model.Email, model.Password, persistCookie: model.RememberMe))
            {
                return RedirectToAction("Index", "Profile");
            }

            ModelState.AddModelError("", "Wrong password or login");

            return View(model);
        }

        [NoCache]
        [AllowAnonymous]
        public ActionResult Register()
        {
            if (WebSecurity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (WebSecurity.IsAuthenticated) RedirectToAction("Index", "Home", null);

            if (ModelState.IsValid)
            {
                try
                {
                    WebSecurity.CreateUserAndAccount(model.Email, model.Password,
                        new { FirstName = model.FirstName, LastName = model.LastName });
                    WebSecurity.Login(model.Email, model.Password);
                    return RedirectToAction("Index", "Home");

                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", "The same user has already created");
                    return View(model);
                }
            }
            return View(model);
        }


        [HttpPost]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        [Authorize]
        public ActionResult EditProfile()
        {
            ViewBag.CurrentEmail = WebSecurity.CurrentUserName;
            ViewBag.CurrentName = Helper.UserIdentity.FirstName + " " + Helper.UserIdentity.LastName;

            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult ChangePassword(string currentPassword, string newPassword)
        {
            string userName = WebSecurity.CurrentUserName;
            bool operationResult = WebSecurity.ChangePassword(userName, currentPassword, newPassword);

            return Json(new { Result = operationResult });
        }

        
        [HttpPost]
        [Authorize]
        public ActionResult ChangeEmail(string newEmail)
        {
            IKernel kernel = new StandardKernel(new DataModelCreator());
            try
            {
                if (!WebSecurity.UserExists(newEmail))
                {
                    var users = kernel.Get<IRepository<User>>();
                    User user = users.GetById(WebSecurity.CurrentUserId);
                    user.Email = newEmail;

                    users.SaveChanges(user);

                    FormsAuthentication.SetAuthCookie(newEmail, ((FormsIdentity)User.Identity).Ticket.IsPersistent);
                }
                else return Json(new { Result = false, Msg = "The same email's already existed in database" });
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                return Json(new { Result = false, Msg = "The email is incorrect" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Msg = ex.Message });
            }

            return Json(new { Result = true, Msg = "Operation complited" });
        }
        

        [HttpPost]
        [Authorize]
        public ActionResult ChangeName(string newFirstName, string newLastName)
        {
            IKernel kernel = new StandardKernel(new DataModelCreator());

            try
            {
                var users = kernel.Get<IRepository<User>>();
                User user = users.GetById(WebSecurity.CurrentUserId);
                if(newFirstName != "")
                    user.FirstName = newFirstName;

                if(newLastName != "")
                    user.LastName = newLastName;

                users.SaveChanges(user);
            }
            catch (Exception ex)
            {
                return Json(new { Result = false });
            }

            return Json(new { Result = true });
        }

    }
}