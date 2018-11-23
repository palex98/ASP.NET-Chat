using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebChat.Models
{
    public class Message
    {
        int Id { set; get; }
        string Text { set; get; }
        DateTime Time { set; get; }
    }
}