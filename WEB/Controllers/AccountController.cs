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

        //это или то!!????!!!
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
            //if (User.Identity.IsAuthenticated) return Content(User.Identity.Name + "(" + User.Identity.GetUserRole() + ")");
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
                //пользователь если сущ
                if (accountservice.UserExist(model.Name))
                {
                    //test expressionvisitor
                   // accountservice.Test();

                    //получить юзера
                    var user = accountservice.GetUserByName(model.Name);
                    bool isrole = accountservice.CheckUserForRole(model.Name,model.Role);
                    if (isrole && Crypto.VerifyHashedPassword(user.Password, model.Password))
                    {
                        ClaimsIdentity claim = new ClaimsIdentity("ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                        claim.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.String));
                        claim.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name, ClaimValueTypes.String));
                        claim.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                            "OWIN Provider", ClaimValueTypes.String));
                        claim.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, model.Role, ClaimValueTypes.String));

                        AuthenticationManager.SignOut();
                        AuthenticationManager.SignIn(new AuthenticationProperties
                        {
                            IsPersistent = true
                        }, claim);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                    ViewBag.Roles = accountservice.GetAllNames();
                }
            }
              
            
            return View(model);
        }
        [HttpGet]
        [ValidateAntiForgeryToken]
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
                //if (accountservice.FindByName(model.Name) == null)
                if (!(accountservice.UserExist(model.Name) && accountservice.UserEmailExist(model.Email)))
                {
                    var blluser = Maper.ToBllUser(model);
                    blluser.TimeRegister = DateTime.Now;
                    accountservice.CreateUser(blluser);
                    return RedirectToAction("Index", "Home");
                }
               ModelState.AddModelError("", "Пользователь с таким логином или электронной почтой уже существует");
            }

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