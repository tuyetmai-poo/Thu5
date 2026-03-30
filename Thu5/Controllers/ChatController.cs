using Microsoft.AspNetCore.Mvc;
using Thu5.Models;

namespace Thu5.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return View(Data.Messages);
        }

        [HttpPost]
        public IActionResult Send(string content)
        {
            Data.Messages.Add(new Message
            {
                Id = Data.Messages.Count + 1,
                Sender = "Customer",
                Content = content
            });

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Reply(string content)
        {
            Data.Messages.Add(new Message
            {
                Id = Data.Messages.Count + 1,
                Sender = "Support",
                Content = content
            });

            return RedirectToAction("Index");
        }
    }
}
