using BLL.Interface.Services;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using BLL.Interface.BLLModel;
using WEB.Models;
using AutoMapper;
using BLL.Interface;
using System.Threading;
using System.Security.Principal;
using WEB.Attribute;
using WEB.WebMappers;
using System.Web.Helpers;
using System.Web.UI;
using WEB.Classes;

namespace WEB.Controllers
{
    [Authorize(Roles = "User,Admin")]
    public class CabinetController : Controller
    {
        private readonly ICabinetService cabinetservice;
        public CabinetController(ICabinetService cabinetservice)
        {
            this.cabinetservice = cabinetservice;
        }

        private ClaimsPrincipal Identiti
        {
            get
            {
                return (ClaimsPrincipal)Thread.CurrentPrincipal;
            }
        }


        #region Lots

        [HttpGet]
        public ActionResult ListAllLotNames()
        {
            var lots = cabinetservice.GetAllLotNameForUserId(Identiti.Identity.GetUserId<int>());
            if (lots.FirstOrDefault() == null)
            {
                ViewBag.result = "Lots not found";
            }
            return View(lots);
        }


        [HttpGet]
        public ActionResult GetLot()
        {
            var lot = cabinetservice.GetLotByName(Request.Params["name"]);
            var lotmodel =  Maper.ToLotModel(lot);
            lotmodel.Cathegory = cabinetservice.GetCathegoryNameById(lot.CathegoryId);
            ViewBag.Cathegories = cabinetservice.GetAllCathegoryNames().Where(name => name != "Все категории");
            return View("ShowLot", lotmodel);
        }

        [HttpGet]
        public ActionResult CreateLot()
        {
            ViewBag.Cathegories = cabinetservice.GetAllCathegoryNames().Where(name=>name!="Все категории");
            return View();
        }


        [HttpPost]
        public ActionResult CreateLot(LotModel lotmodel)
        {
            if (ModelState.IsValid)
            {
                lotmodel.Statys = Statys.InProcess;

                if (cabinetservice.GetLotByName(lotmodel.Name) == null)
                {
                    var blllot = Maper.ToBllLot(lotmodel);
                    blllot.CathegoryId = cabinetservice.GetCathegoryIdByName(lotmodel.Cathegory);

                    cabinetservice.CreateLot(blllot);
                    var LotId = cabinetservice.GetLotIdByName(lotmodel.Name);
                    ViewBag.LotId = LotId;
                    return View("ImageLoad");
                }
                else ModelState.AddModelError("", "Лот с таким названием уже существует");
            }
            else ModelState.AddModelError("", "Incorrect input Data");

            ViewBag.Cathegories = cabinetservice.GetAllCathegoryNames().Where(name => name != "Все категории");
            return View(lotmodel);
        }


        [HttpGet]
        public ActionResult ImageLoad()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImageLoad(int Id, HttpPostedFileBase LoadImage)
        {
            if (ModelState.IsValid && LoadImage != null)
            {
                cabinetservice.CreateImage(Image.CreateBllImage(Id, LoadImage));
                 return RedirectToAction("ListAllLotNames");
            }
            else
            {
                ModelState.AddModelError("","Check input data");
                ViewBag.LotId = Id;
                return View();
            }
        }
        public ActionResult GetImage(int Id)
        {
            var imagemodel = cabinetservice.GetImagesByLotId(Id).Select(image => Maper.ToImageModel(image));
            ViewBag.LotsId = Id;
            if (imagemodel.FirstOrDefault() != null)
            {
                return View(imagemodel);
            }
            else
            {
                return View();
            }
        }

        public ActionResult DeleteImage()
        {
            var Id = Convert.ToInt32(Request.Url.Segments[3]);
            cabinetservice.DeleteImageById(Id);
            var lot = cabinetservice.GetLotById(Convert.ToInt32(Request.Params["lotid"]));
            return View("ShowLot",Maper.ToLotModel(lot));
        }

        [HttpPost]
        public ActionResult AddImage(int Id, HttpPostedFileBase Load)
        {
            var lotmodel = cabinetservice.GetLotById(Id);
            if (ModelState.IsValid && Load != null)
            {
                cabinetservice.CreateImage(Image.CreateBllImage(Id, Load));
                return View("ShowLot", Maper.ToLotModel(lotmodel));
            }
            else 
            {
                ModelState.AddModelError("","Неправильный ввод данных");
                return View("ShowLot", Maper.ToLotModel(lotmodel));
            }
        }


        [HttpPost]
        [Button("Edit")]
        [ActionName("GetLot")]
        public ViewResult EditLot(LotModel lotmodel)
        {
            ViewBag.name = lotmodel.Name;
            ViewBag.Cathegories = cabinetservice.GetAllCathegoryNames().Where(name => name != "Все категории");
            return View("UpdateLot",lotmodel);
        }

        public ActionResult UpdateImages(int Id)
        {
            var ImagesModel = cabinetservice.GetImagesByLotId(Id);
            return View(ImagesModel);
        }


        [HttpPost]
        public ActionResult UpdateLot(LotModel lotmodel)
        {
            if (ModelState.IsValid)
            {
                var blllot = Maper.ToBllLot(lotmodel);         
                blllot.CathegoryId = cabinetservice.GetCathegoryIdByName(lotmodel.Cathegory);
                cabinetservice.UpdateLot(blllot);
                return RedirectToAction("ListAllLotNames");
            }
            else ModelState.AddModelError("", "Incorrect input data");
            return View(lotmodel);
        }


        [HttpPost]
        [Button("Delete")]
        [ActionName("GetLot")]
        public ActionResult DeleteLot(LotModel lotmodel)
        { 
            cabinetservice.DeleteLot(lotmodel.Name);
            return RedirectToAction("ListAllLotNames");
        }

        #endregion

        #region Settings

        [HttpGet]
        public ActionResult Settings()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult UpdateNikneim()
        {
            var user = cabinetservice.GetUserById(Identiti.Identity.GetUserId<int>());
            ViewBag.Name = user.Name;
            return PartialView(Maper.ToUserModel(user));
        }



        [HttpPost]
        public PartialViewResult UpdateNik(UserModel usermodel)
        {
            if (ModelState.IsValid)
            {
                if (!cabinetservice.UserExist(usermodel.Name))
                {
                        if (usermodel.Name != Identiti.Identity.Name)
                        {
                            cabinetservice.UpdateUser(Maper.ToBllUserFromUserModel(usermodel));
                            ((ClaimsIdentity)Identiti.Identity).RemoveClaim(((ClaimsIdentity)Identiti.Identity).FindFirst(ClaimTypes.Name));
                            ((ClaimsIdentity)Identiti.Identity).AddClaim(new Claim(ClaimTypes.Name, usermodel.Name));
                            ViewBag.IsSuccessed = "Success";

                            return PartialView("UpdateNikneim", usermodel);
                        }
                        else ModelState.AddModelError("", "Данные не изменились");
                }
                else ModelState.AddModelError("", "Пользователь с таким Ником уже существует");
            }
            else ModelState.AddModelError("", "Неправильный Никнеим");
                Response.StatusCode = 400;
                return PartialView("UpdateNikneim", usermodel);

        }


        [HttpPost]
        public PartialViewResult UpdatePassword(NewPasswordModel pasmodel)
        {
            if (ModelState.IsValid)
            {
                var user = cabinetservice.GetUserById(Identiti.Identity.GetUserId<int>());

                    if (Crypto.VerifyHashedPassword(user.Password, pasmodel.Password))
                    {
                        user.Password = Crypto.HashPassword(pasmodel.NewPassword);
                        cabinetservice.UpdateUser(user);
                        return PartialView();
                    }
                    else ModelState.AddModelError("", "Пароли не совпадают");

            }
            else ModelState.AddModelError("","Неверный ввод пароля");
            Response.StatusCode = 400;
            return PartialView();
        }




        [ChildActionOnly]
        public PartialViewResult UpdateProfile()
        {

            ViewBag.countries = cabinetservice.GetAllCountries();
            var bllprofile = cabinetservice.FindUserProfile(Identiti.Identity.Name);
            if (bllprofile!=null)
            {
                var profmodel = Maper.ToProfileModel(bllprofile);
                profmodel.Country = cabinetservice.GetCountryNameById(bllprofile.CountryId); 
                return PartialView(profmodel);
            }
                return PartialView();
        }


        [HttpPost]  
        public PartialViewResult UpdateProf(ProfileModel profmodel)
        {
            ModelState.Remove("Id");
            if (ModelState.IsValid)
           {
            var countryId = cabinetservice.GetIdByCountryName(profmodel.Country);
            var bllprof = Maper.ToBllProfile(profmodel);
            bllprof.CountryId = countryId;

            if (!cabinetservice.UserHasProfile(Identiti.Identity.Name))
                {
                    var id = cabinetservice.FindIdByName(Identiti.Identity.Name);
                    bllprof.Id = id;
                    cabinetservice.CreateProfile(bllprof);
                       profmodel.Id = id;
                }
                else
                {
                bllprof.Id = cabinetservice.GetUserIdByName(Identiti.Identity.Name);
                    cabinetservice.UpdateProfile(bllprof);
                }
            }
            else ModelState.AddModelError("", "Профиль некорректен");
                ViewBag.countries = cabinetservice.GetAllCountries();
            return PartialView("UpdateProfile", profmodel);
        }


        #endregion

    }
}