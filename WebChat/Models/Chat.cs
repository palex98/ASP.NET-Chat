using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace WebChat.Models
{
    public static class Chat
    {
        public static List<User> Users;
        public static List<Message> Messages;
        public static int UserId = 0;
        static Thread MonitoringListOfUsers;
        public static object Userlocker = new object();
        public static object Messagelocker = new object();

        static Chat()
        {
            Users = new List<User>();
            Messages = new List<Message>();

            MonitoringListOfUsers = new Thread(new ThreadStart(Monitoring));
            MonitoringListOfUsers.Start();
        }

        static void Monitoring()
        {
            while (true)
            {
                lock (Userlocker)
                {
                    foreach (var u in Users)
                    {
                        if ((DateTime.Now - u.LastActivity).Seconds > 10)
                        {
                            Users.Remove(u);
                            break;
                        }
                    }
                }
                Thread.Sleep(1000);
            }
        }
    }
}