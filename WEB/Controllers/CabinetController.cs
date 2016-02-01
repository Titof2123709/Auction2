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
        //список лотов
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

        //отображает конкретный лот
        [HttpGet]
        public ActionResult GetLot()
        {
            var lot = cabinetservice.GetLotByName(Request.Params["name"]);

          return View("ShowLot", Maper.ToLotModel(lot));
        }

        [HttpGet]
        public ActionResult CreateLot()
        {
            return View();
        }

        //создание нового лота
        [HttpPost]
        public ActionResult CreateLot(LotModel lotmodel)
        {
            if (ModelState.IsValid)
            {                
                //это не должно быть
         lotmodel.Statys = Statys.InProcess;

                if (cabinetservice.GetLotByName(lotmodel.Name) == null)
                {
                    var blllot = Maper.ToBllLot(lotmodel);

                    //это не должно быть
                    blllot.CathegoryId = 3;


                    cabinetservice.CreateLot(blllot);
                  //  ListAllLotNames();
                   // return View("ListAllLotNames");
                   var LotId = cabinetservice.GetLotByName(lotmodel.Name).Id;
                    ViewBag.LotId = LotId;
                    return View("ImageLoad");
                }
                else ModelState.AddModelError("", "Лот с таким названием уже существует");
            }
            return View(lotmodel);
        }


        [HttpGet]
        public ActionResult ImageLoad()
        {
            return View();
        }

        //загрузка изображения
        [HttpPost]
        public ActionResult ImageLoad(int Id, HttpPostedFileBase LoadImage)
        {
            if (ModelState.IsValid && LoadImage != null)
            {
                BllImage bllimage = new BllImage();
                bllimage.MimeType = LoadImage.ContentType;
                bllimage.Image = new byte[LoadImage.ContentLength];
                bllimage.LotId = Id;
                LoadImage.InputStream.Read(bllimage.Image, 0, LoadImage.ContentLength);
                LoadImage.InputStream.Close();        

                cabinetservice.CreateImage(bllimage);
               // TempData["message"] = string.Format("{0} has been saved", imagemodel.Name);
              //   ListAllLotNames();
               //  return View("ListAllLotNames");
                 return RedirectToAction("ListAllLotNames");
            }
            else
            {
                ModelState.AddModelError("","Check input data");
                return View();
            }
        }
        public ActionResult GetImage(int Id)
        {
            var imagemodel = cabinetservice.GetImagesByLotId(Id).Select(image => Maper.ToImageModel(image));
            if (imagemodel != null)
            {
                return View(imagemodel);
            }
            else
            {
                return Content("<h4>Image not found<h4>");
            }
        }

        //редактирование лота
        [HttpPost]
        [Button("Edit")]
        [ActionName("GetLot")]
        public ViewResult EditLot(LotModel lotmodel)
        {
            ViewBag.name = lotmodel.Name;
            return View("UpdateLot",lotmodel);
        }

     /*   Метод PUT применяется для обновления информации на сервере, 
          и требует, чтобы содержимое запроса HTTP PUT сохранялось на сервере.
        */


        public ActionResult UpdateImages(int Id)
        {
            var ImagesModel = cabinetservice.GetImagesByLotId(Id);
            return View(ImagesModel);
        }


        //закоментил но нужна проверка!!!!
        [HttpPost]
        [Button("Next")]
        [ActionName("GetLot")]
        public ActionResult UpdateLot(LotModel lotmodel)
        {            //нужно знать ещё и старое имя чтоб было что удалять...!!!!!
            int id = cabinetservice.GetLotByName(Request.Params["param"]).Id;
            var blllot = Maper.ToBllLot(lotmodel);
            blllot.Id = id;
            cabinetservice.UpdateLot(blllot);

            //получаем изображения загруженные для данного лота
            var imagemodels = cabinetservice.GetImagesByLotId(id); 

          //  ListAllLotNames();
          //  return View("ShowLot", lotmodel);
            return View("UpdateImages",imagemodels);
        }

        //удаление лота
        //НЕ РЕАЛИЗОВАНО!!!!!
        [HttpPost]
        [Button("Delete")]
        [ActionName("GetLot")]
        public ActionResult DeleteLot(LotModel lotmodel)
        {   //удалить из бд лот с таким-то именем iduser и таким-то name лота(имена лотов у одного юзера не совпадают)
           // ListAllLotNames();
           // return View("ListAllLotNames");
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


       // [PreventSpam(DelayRequest = 60, ErrorMessage = "You can only create a new widget every 60 seconds.")]
        [HttpPost]
        public PartialViewResult UpdateNik(UserModel usermodel)
        {
            if (ModelState.IsValid)
            {
                //если пользователя с таким ником нет
                if (!cabinetservice.UserExist(usermodel.Name))
                {
                    //если старое и новое имя не равны!!
                        if (usermodel.Name != Identiti.Identity.Name)
                        {
                            cabinetservice.UpdateUser(Maper.ToBllUserFromUserModel(usermodel));
                            //здесь ещё надо как-то изменить claim имя пользователя!!!
                            //  ClaimsPrincipal.Current.Claims
                            //не работает!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                            ((ClaimsIdentity)Identiti.Identity).RemoveClaim(((ClaimsIdentity)Identiti.Identity).FindFirst(ClaimTypes.Name));
                            ((ClaimsIdentity)Identiti.Identity).AddClaim(new Claim(ClaimTypes.Name, usermodel.Name));
                            ViewBag.IsSuccessed = "Success";

                            return PartialView("UpdateNikneim", usermodel);
                        }
                        else ModelState.AddModelError("", "Данные не изменились");
                }
                else ModelState.AddModelError("", "Пользователь с таким Ником уже существует");
            }
            //проверить как это работает!!!
            //не показывает!!!
            else ModelState.AddModelError("", "Неправильный Никнеим");

            //для неуспешного ответа ajax form
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
                        //не показывает
                    else ModelState.AddModelError("", "Пароли не совпадают");

            }
                //проверить как это работает!!!
                //не показывает!!!
            else ModelState.AddModelError("","Неверный ввод пароля");
            Response.StatusCode = 400;
            return PartialView();
        }




        [ChildActionOnly]
        public PartialViewResult UpdateProfile()
        {
            //здесь вот exeption если стран нет!!они должны быть подгружены!!
            ViewBag.countries = cabinetservice.GetAllCountries();
            //поиск профиля в бд для текущего юзера
            var bllprofile = cabinetservice.FindUserProfile(Identiti.Identity.Name);
            if (bllprofile!=null)
            {
                var profmodel = Maper.ToProfileModel(bllprofile);
                //добавляем страну для профиля!
                profmodel.Country = cabinetservice.GetCountryNameById(bllprofile.CountryId); 
                return PartialView(profmodel);
            }
                return PartialView();
        }


        [HttpPost]                       //незабыть провалидировать!!
        public PartialViewResult UpdateProf(ProfileModel profmodel)
        {
            ModelState.Remove("Id");
            if (ModelState.IsValid)
           {
            //поиск id для выбранной страны! 
            var countryId = cabinetservice.GetIdByCountryName(profmodel.Country);
            var bllprof = profmodel.ToBllProfile();
            bllprof.CountryId = countryId;
                //какая-то логика что id == 0 если не найдено в методе UpdateProfile профиля ещё не было!!!
            if (!cabinetservice.UserHasProfile(Identiti.Identity.Name))
                {
                    //поиск пользователя Id для которого добавл профиль 
                    var id = cabinetservice.FindIdByName(Identiti.Identity.Name);
                    bllprof.Id = id;
                    cabinetservice.CreateProfile(bllprof);

                    //установить Id
                    //это чтобы было видно что новый профиль сформирован
                    //и если снова обратиться к измен профиля то не заходить
                    //в этот if(id==0)
                       profmodel.Id = id;
                }
                else
                {
                bllprof.Id = cabinetservice.GetUserIdByName(Identiti.Identity.Name);
                    //обновить новый профиль в бд
                    cabinetservice.UpdateProfile(bllprof);
                }
            }
            //проверить как это работает!!!
            //не показывает!!!
            else ModelState.AddModelError("", "Профиль некорректен");
            //снова получить список стран!
                ViewBag.countries = cabinetservice.GetAllCountries();
            return PartialView("UpdateProfile", profmodel);
        }


        #endregion

    }
}