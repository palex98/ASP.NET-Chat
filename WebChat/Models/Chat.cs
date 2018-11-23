using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebChat.Models
{
    public static class Chat
    {
        public static List<User> Users;
        public static List<Message> Messages;
        public static int Id = 0;

        static Chat()
        {
            Users = new List<User>();
            Messages = new List<Message>();
        }
    }
}