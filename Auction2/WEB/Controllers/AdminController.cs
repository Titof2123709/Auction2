using BLL.Interface;
using BLL.Interface.BLLModel;
using BLL.Interface.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB.Attribute;
using WEB.Models;
using WEB.WebMappers;

namespace WEB.Controllers
{
   [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
       private readonly IAdminService adminservice;
       public AdminController(IAdminService adminservice)
        {
            this.adminservice = adminservice;
        }


       public ActionResult AdminRoom()
       {
           return View();
       }


       #region Lots

       public ActionResult AdminLots()
       {
           var lots = adminservice.GetAllLots();
           var lotmodels =lots.Select(lot=>Maper.ToLotModel(lot)).Select(lm => 
           {
               lm.Cathegory = adminservice.GetCathegoryNameById(lots.Where(lt => lt.Id == lm.Id).FirstOrDefault().CathegoryId);
               return lm;
           });
           Response.StatusCode = Request.Params["response"] == "400" ? 400 : 200;
            return View(lotmodels);
       }

 
       public ActionResult DeleteLot(int Id)
       {
           adminservice.DeleteLotById(Id);
           ViewBag.number = 2;
           return View("AdminRoom");
       }

     /*  [HttpPost]
       [Button("UpdateLot")]
       [ActionName("AdminRoom")]
       public ActionResult UpdateLot(int Id)
       {   
           adminservice.DeleteLotById(Id);
           return RedirectToAction("UserRoles", "Admin", new { id = Id });

           return View();
       }
        */

       public ActionResult _GetLotOwner(int Id)
       {
           var owner = adminservice.GetUserByLotId(Id);
           return PartialView(Maper.ToUserModel(owner));
       }

       #endregion


       #region AdminUsers

        public ActionResult AdminUsers()
        {
            ViewBag.number = 1;
            Response.StatusCode = Request.Params["response"] == "400" ? 400 : 200;
            var users = adminservice.GetAllUsers();
            return View(users.Select(user=>Maper.ToUserModel(user)));
        }

        [HttpPost]
        [Button("Delete")]
        [ActionName("AdminRoom")]
        public ActionResult DeleteUser(int Id)
        {
            try
            {
                if (Id != 1) adminservice.DeleteUserById(Id);
            }
            catch
            {
                return View("ExceptionUserDelete");
            }
            return View();
        }

        [HttpPost]
        [Button("Roles")]
        [ActionName("AdminRoom")]
        public ActionResult GetUserRoles(int Id)
        {
            return RedirectToAction("UserRoles", "Admin", new { id = Id });
        }


        public ActionResult GetUserProfile(int Id)
        {
            var profile = adminservice.GetProfileByUserId(Id);
            if (profile != null)
            {
                string country = adminservice.GetCountryForProfile(profile.CountryId);
                var profilemodel = Maper.ToProfileModel(profile);
                profilemodel.Country = country;
                return PartialView("_UserProfile", profilemodel);
            }
            return PartialView("_UserProfile");
        }

        #endregion


        #region Roles

        [ChildActionOnly]
        public ActionResult _ShowRolesForUser(int Id)
        {
            ViewBag.UserId = Id;
            var roles = adminservice.GetAllUserRoles(Id);
            return View(roles.Select(role=>Maper.ToRoleModel(role)));
        }

        [ChildActionOnly]
        public ActionResult _ShowAllRoles()
        {
            var roles = adminservice.GetAllRoles();
            return PartialView(roles.Select(role => Maper.ToRoleModel(role)));
        }

        public ActionResult UserRoles(int id)
        {
            int Id = Convert.ToInt32(id);
            ViewBag.UserId = Id;
            return View();
        }

        [ChildActionOnly]         
        public ActionResult AllRoles()
        {
            return PartialView();
        }

        public ActionResult AddUserRole(string Name,int UserId)
        {
             if (ModelState.IsValid)
            {
                if (adminservice.RoleExists(Name) && (!adminservice.UserHasRole(Name, UserId)))
                {
                 adminservice.AddRoleForUserByUserId(Name,UserId);
                }
                else Response.StatusCode = 400;
            }
             else ModelState.AddModelError("", "Неправильный ввод данных");
             var roles = adminservice.GetAllUserRoles(UserId);
             return View("_ShowRolesForUser", roles.Select(role => Maper.ToRoleModel(role)));
        }

        public ActionResult DeleteUserRole(string Name,int UserId)
        {
           if (ModelState.IsValid)
            {
                if (adminservice.RoleExists(Name) && adminservice.UserHasRole(Name, UserId))
                {
                 adminservice.DeleteRoleForUserByUserIdAndRoleName(UserId,Name);
                }
                else Response.StatusCode = 400;
            }
           else ModelState.AddModelError("", "Неправильный ввод данных");

           var roles = adminservice.GetAllUserRoles(UserId);
           return View("_ShowRolesForUser", roles.Select(role => Maper.ToRoleModel(role)));
        }


        public ActionResult AddNewRole(RoleModel rolemodel)
        {
            if (ModelState.IsValid)
            {
                if (!adminservice.RoleExists(rolemodel.Name))
                {
                    adminservice.AddRole(Maper.ToBllRole(rolemodel));
                }
                else Response.StatusCode = 400;
            }
            else ModelState.AddModelError("", "Неправильный ввод данных"); 
            var roles = adminservice.GetAllRoles();
            return View("_ShowAllRoles",roles.Select(role=>Maper.ToRoleModel(role)));
        }


        public ActionResult DeleteFromAllRole(string Name)
        {
            if (ModelState.IsValid)
            {
                if (adminservice.RoleExists(Name) && Name != "Admin")
                {
                    adminservice.DeleteRoleByName(Name);
                }
                else Response.StatusCode = 400;
            }
            else ModelState.AddModelError("", "Неправильный ввод данных");
            var roles = adminservice.GetAllRoles();
            return View("_ShowAllRoles", roles.Select(role => Maper.ToRoleModel(role)));
        }


        public ActionResult UpdateRole(RoleModel rolemodel)
        {
            if (ModelState.IsValid)
            {
                if (adminservice.RoleExists(rolemodel.Name))
                {
                    adminservice.UpdateRoleForUser(Maper.ToBllRole(rolemodel));
                }
                else Response.StatusCode = 400;
            }
            else ModelState.AddModelError("", "Неправильный ввод данных");
            var roles = adminservice.GetAllRoles();
            return View("_ShowAllRoles", roles.Select(role => Maper.ToRoleModel(role)));
        }

        #endregion


        #region Cathegories

        public ActionResult BarLots()
        {
            var cathegories = adminservice.GetAllCathegoryNames();
            ViewBag.Cathegories = cathegories;
            return View(cathegories);
        }

        public ActionResult GetAllLots()
        {
            return RedirectToAction("AdminLots");
        }

        public ActionResult GetLotsByCathegory(string cathegory)
        {
            IEnumerable<BllLot> lots;
            if (cathegory == "Все категории")
            {
                lots = adminservice.GetAllLots();
            }
            else
            {
                lots = adminservice.GetLotsByCathegoryName(cathegory);
            }
            if(lots.Count()==0)
            {
                return View("AdminLots");
            }
            return View("AdminLots", lots.Select(lot=>Maper.ToLotModel(lot)));
        }


        public ActionResult BarUsers()
        {
            var roles = adminservice.GetAllRoleNames();
            ViewBag.Roles = roles;
            return View();
        }

        public ActionResult GetAllUsers()
        {
            return RedirectToAction("AdminUsers");
        }

        public ActionResult GetUsersByRole(string rol)
        {
           var users = adminservice.GetUsersByRoleName(rol);
           if (users.Count() == 0)
           {
               return View("AdminUsers");
           }
           return View("AdminUsers", users.Select(user => Maper.ToUserModel(user)));
        }


        #endregion


        #region Search


        public ActionResult FindUser()
        {
            string search = Request.Params["Find"];
            var blluser = adminservice.FindUserByName(search);
            if (blluser.Count() == 0)
            { 
                return RedirectToAction("AdminUsers", "Admin", new { response = 400 });
            }
            Response.StatusCode = 200;
            return View("AdminUsers", blluser.Select(user=>Maper.ToUserModel(user)));
        }
        
        public ActionResult FindLot()
        {
            string search = Request.Params["Find"];
            var blllot = adminservice.FindLotByName(search);
            if (blllot.Count() == 0)
            {
                return RedirectToAction("AdminLots", "Admin", new { response = 400 });
            }
            Response.StatusCode = 200;
            return View("AdminLots", blllot.Select(lot=>Maper.ToLotModel(lot)));
        }
        

        public ActionResult FindLotByNik()
        {
            string search = Request.Params["Nik"];
            var blllot = adminservice.FindLotsByUserName(search);
            if (blllot.Count() == 0)
            {
                return RedirectToAction("AdminLots", "Admin", new { response = 400 });
            }
            Response.StatusCode = 200;
            return View("AdminLots", blllot.Select(lot=>Maper.ToLotModel(lot)));
        }

       
        #endregion
    }
}