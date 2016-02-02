using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB.Models;
using BLL.Interface.BLLModel;
using BLL.Interface.Services;
using AutoMapper;
using System.Web.Security;
using WEB.Infrastuctura;
using System.Web.Helpers;
using Microsoft.Owin.Security;
using System.Security.Claims;
using WEB.WebMappers;
using System.Threading;
using WEB.Classes;

namespace WEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService accountservice;

        public AccountController(IAccountService accountservice)
        {
            this.accountservice = accountservice;
        }


        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        private IClaimsAutorization Authenticate
        {
            get { return new ClaimsAutorization(); }
        }
        private ClaimsPrincipal Identiti
        {
            get
            {
                return (ClaimsPrincipal)Thread.CurrentPrincipal;
            }
        }


        [ChildActionOnly]
        public ActionResult Nikneim()
        {
            if (User.Identity.IsAuthenticated) return Content(Identiti.Identity.Name + "(" + Identiti.Identity.GetUserRole() + ")");
            return Content("Anonymous");
        }

        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.Roles = accountservice.GetAllNames();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (accountservice.UserExist(model.Name))
                {
                    var user = accountservice.GetUserByName(model.Name);
                    bool isrole = accountservice.CheckUserForRole(model.Name,model.Role);
                    if (isrole && Crypto.VerifyHashedPassword(user.Password, model.Password))
                    {
                        Authenticate.Autorization(model, user.Id);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                    
                }
            }
            ModelState.AddModelError("", "Incorrect input data");
            ViewBag.Roles = accountservice.GetAllNames();
            return View();
        }


        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (!(accountservice.UserExist(model.Name) && accountservice.UserEmailExist(model.Email)))
                {
                    var blluser = Maper.ToBllUser(model);
                    blluser.TimeRegister = DateTime.Now;
                    accountservice.CreateUser(blluser);
                    return RedirectToAction("Index", "Home");
                }
               ModelState.AddModelError("", "Пользователь с таким логином или электронной почтой уже существует");
            }
            ModelState.AddModelError("", "Incorrect input data");

            return View(model);
         }

        public ActionResult Logoff()
        {
            if (User.Identity.IsAuthenticated)
            {
                AuthenticationManager.SignOut();
            }
            return RedirectToAction("Index", "Home");
        }

    }
}