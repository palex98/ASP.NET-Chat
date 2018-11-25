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
            HttpContext.Response.Cookies["LastMessage"].Value = 0.ToString();
            HttpContext.Response.Cookies["UserNickname"].Value = "";
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(string nickname)
        {

            if (NicknameIsBusy(nickname)) return PartialView("ReLogIn");

            lock (Chat.Userlocker)
            {
                Chat.Users.Add(new User { Id = Chat.UserId++, Nickname = nickname, LastActivity = DateTime.Now });
            }

            HttpContext.Response.Cookies["UserNickname"].Value = nickname;
            ViewBag.Nick = nickname;
            return PartialView();
        }

        bool NicknameIsBusy(string Nick)
        {
            User R;
            try
            {
                R = Chat.Users.Single(a => a.Nickname == Nick);
            }
            catch
            {
                return false;
            }
            return true;
        }

        [HttpGet]
        public ActionResult GetListOfUsers(string user)
        {
            ViewBag.Users = Chat.Users;

            return PartialView("ListOfUsers");
        }

        [HttpPost]
        public ActionResult PostMessage(string text)
        {
            if (text.Length > 0 && text != "\n")
            {
                text = EmojiParse(text);
                lock (Chat.Messagelocker)
                {
                    Chat.Messages.Add(new Message
                    {
                        Author = HttpContext.Request.Cookies["UserNickname"].Value,
                        Text = text,
                        DateTime = DateTime.Now.ToLongTimeString()
                    });
                }
            }
            ViewBag.Messages = new List<Message>();
            return PartialView("Messages");
        }

        string EmojiParse(string text)
        {
            text = text.Replace(":-)", ";1;")
                .Replace(">:-(", ";3;")
                .Replace(":-(", ";2;")
                .Replace(":-<", ";4;")
                .Replace("&-<", ";5;")
                .Replace(">:-|", ";6;")
                .Replace(";-)", ";7;")
                .Replace(";-D", ";8;");
            return text;
        }

        [HttpGet]
        public ActionResult GetListOfMessages()
        {

            string fromUser = null;

            try
            {
                fromUser = HttpContext.Request.Cookies["UserNickname"].Value;
            }
            catch
            {
                ViewBag.Messages = Chat.Messages;
                return PartialView("Messages");
            }

            try
            {
                Chat.Users.Single(a => a.Nickname == fromUser).LastActivity = DateTime.Now;
            }
            catch
            {
                if (fromUser != "")
                {
                    Chat.Users.Add(new User
                    {
                        Id = Chat.UserId++,
                        Nickname = fromUser,
                        LastActivity = DateTime.Now
                    });
                }
            }


            if (Chat.Messages.Count == 0)
            {
                ViewBag.Messages = Chat.Messages;

                return PartialView("Messages");
            }

            lock (Chat.Messagelocker)
            {
                int LastMsg = Convert.ToInt32(HttpContext.Request.Cookies["LastMessage"].Value);

                if (LastMsg == 0 && Chat.Messages.Count <= 10)
                {
                    ViewBag.Messages = Chat.Messages;
                }
                else if (LastMsg == 0 && Chat.Messages.Count > 10)
                {
                    ViewBag.Messages = Chat.Messages.GetRange(Chat.Messages.Count - 10, 10);
                }
                else if (LastMsg != 0)
                {
                    int CountOfNewMessage = Chat.Messages.Count - LastMsg;
                    ViewBag.Messages = Chat.Messages.GetRange(LastMsg, CountOfNewMessage);
                }
            }

            HttpContext.Response.Cookies["LastMessage"].Value = Chat.Messages.Count.ToString();
         
            return PartialView("Messages");
        }
    }
}