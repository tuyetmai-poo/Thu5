using Microsoft.AspNetCore.Mvc;
using Thu5.Models;
using Thu5.Data;
using System.Linq;

namespace Thu5.Controllers
{
    public class ChatController : Controller
    {
        private readonly AppDbContext _context;

        public ChatController(AppDbContext context)
        {
            _context = context;
        }

        // ====== VIEW CHAT (KHÁCH) ======
        public IActionResult Index()
        {
            var messages = _context.Messages
                                   .OrderBy(m => m.Id)
                                   .ToList();

            return View(messages);
        }

        // ====== KHÁCH GỬI ======
        [HttpPost]
        public IActionResult Send(string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                _context.Messages.Add(new Message
                {
                    Sender = "Customer",
                    Content = content
                });

                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // ====== SUPPORT TRẢ LỜI ======
        [HttpPost]
        public IActionResult Reply(string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                _context.Messages.Add(new Message
                {
                    Sender = "Support",
                    Content = content
                });

                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}