using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebChat.Models
{
    public class Message
    {
        public string Author { set; get; }
        public string Text { set; get; }
        public string DateTime { set; get; }
    }
}