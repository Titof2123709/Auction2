using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB.Models;
using WEB.WebMappers;

namespace WEB.Controllers
{
    [Authorize(Roles = "User,Admin")]
    public class AuctionController : Controller
    {
        //десь SignalR и Timer 
        // GET: Auction
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Chat(string name)
        { 
            //http://metanit.com/sharp/mvc/3.8.php
            if (name == null) return Redirect("/Shared/Error");
            //здесь мы как-то получаем название товара 
            return View(new ChatModel() { Name = User.Identity.Name, GroupName = name });
        }

    }
}