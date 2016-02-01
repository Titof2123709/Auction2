using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB.Models;
using BLL.Interface.BLLModel;
using BLL.Interface.Services;
using System.Web.Security;
using System.Net;
using Microsoft.Owin.Security;
using Thinktecture.IdentityModel.Authorization.Mvc;
using System.Security.Claims;
using AutoMapper;
using BLL.Interface;
using PagedList;
using WEB.WebMappers;

namespace WEB.Controllers
{     
    [Authorize(Roles = "User,Admin")]
    public class HomeController : Controller
    {
        private readonly IMainService mainservice;
        public HomeController(IMainService mainservice)
        {
            this.mainservice = mainservice;
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult RedirectToCabinet()
        {
            return RedirectToAction("ListAllLotNames", "Cabinet");
        }
        public ActionResult RedirectToAdminCabinet()
        {
            return RedirectToAction("Index", "Admin");
        }
        public ActionResult RedirectToChat()
        {
            return RedirectToAction("Chat", "Auction");
        }

        //[ClaimsAuthorize(ClaimTypes.Role,"User,Admin")]
        //получение списка лотов для категории 
        [HttpGet]
        public ActionResult Index(string name,int? page)
        {
            IEnumerable<BllLot> lots;
            if (name == null || name == "Все категории")
            {
                ViewBag.Name = "Все категории";
                lots = mainservice.FindAllExposeLots();
            }
            else
            {
                lots = mainservice.FindLotsByCathegoryNameAndStatysInProcess(name);
                ViewBag.Name = name;
            }
            if (lots.FirstOrDefault() == null) ViewBag.Empty = "Lots Not Found";
            return View(Maper.ToLotModel(lots).ToPagedList((page ?? 1), 5));
        }

        //список категорий
        public ActionResult GetAllCathegories()
        {
           return PartialView(mainservice.GetAllNames());
        }

        public ActionResult FindLot()
        {
          string search = Request.Params["Find"];
           var lotmodel = Maper.ToLotModel(mainservice.FindLotByName(search));
           if (lotmodel.Count() == 0) ViewBag.Empty = "Lots Not Found";
            return View("Index", lotmodel.ToPagedList(1, 1));
        }

    }
}