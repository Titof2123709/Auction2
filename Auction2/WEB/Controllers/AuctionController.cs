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
        //SignalR и Timer 
        // GET: Auction
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Chat(string name)
        { 

            if (name == null) return Redirect("/Shared/Error");

            return View(new ChatModel() { Name = User.Identity.Name, GroupName = name });
        }

    }
}