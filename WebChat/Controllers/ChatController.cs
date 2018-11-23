using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebChat.Models;

namespace WebChat.Controllers
{
    public class ChatController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogIn(string nickname)
        {
            Chat.Users.Add(new User { Id = Chat.Id++, Nickname = nickname });
            ViewBag.Nick = nickname;
            return PartialView();
        }
    }
}