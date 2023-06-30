using Microsoft.AspNetCore.Mvc;
using Diploma.Models;

namespace Diploma.Controllers
{
    public class ContactController : Controller
    {
        private readonly DiplomaContext _context;

        public ContactController(DiplomaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Получение контактной информации из базы данных
            var contacts = _context.Users.FirstOrDefault();
            return View(contacts);
        }
    }
}
