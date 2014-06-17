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

namespace NSEmbroidery.ASP.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (WebSecurity.IsAuthenticated) RedirectToAction("Index", "Home", null);

            if (ModelState.IsValid && WebSecurity.Login(model.Email, model.Password, persistCookie: model.RememberMe))
            {
                return RedirectToAction("Index", "Profile");
            }

            ModelState.AddModelError("", "Wrong password or login");
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
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
        [AllowAnonymous]
        public ActionResult EditProfile()
        {
            ViewBag.CurrentEmail = WebSecurity.CurrentUserName;
            ViewBag.CurrentName = Helper.UserIdentity.FirstName + " " + Helper.UserIdentity.LastName;

            return View();
        }

        [HttpPost]
        //[Authorize]
        public ActionResult ChangePassword(string currentPassword, string newPassword)
        {
            string userName = WebSecurity.CurrentUserName;
            bool operationResult = WebSecurity.ChangePassword(userName, currentPassword, newPassword);

            return View(operationResult);
        }


        [HttpPost]
        [Authorize]
        public ActionResult ChangeEmail(string newEmail)
        {
            IKernel kernel = new StandardKernel(new DataModelCreator());

            var users = kernel.Get<IRepository<User>>();
            User user = users.GetById(WebSecurity.CurrentUserId);
            user.Email = newEmail;

            users.SaveChanges(user);

            return View();
        }


        [HttpPost]
        [Authorize]
        public ActionResult ChangeName(string newFirstName, string newLastName)
        {
            IKernel kernel = new StandardKernel(new DataModelCreator());

            var users = kernel.Get<IRepository<User>>();
            User user = users.GetById(WebSecurity.CurrentUserId);
            user.FirstName = newFirstName;
            user.LastName = newLastName;

            users.SaveChanges(user);

            return View();
        }

    }
}