using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebChat.Controllers;

namespace WebChat.Tests.Controllers
{
    [TestClass]
    public class ChatControllerTest
    {
        [TestMethod]
        public void IndexViewResultNotNull()
        {
            ChatController controller = new ChatController();

           var result = controller.EmojiParse("any text :-)");

            Assert.IsNotNull(result);
        }
    }
}
