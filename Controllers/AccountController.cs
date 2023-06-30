using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Diploma.Models;

namespace Diploma.Controllers
{
    public class AccountController : Controller
    {
        private readonly DiplomaContext _context;

        public AccountController(DiplomaContext context)
        {
            _context = context;
        }

        // GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View();
            }

            return RedirectToAction("Index", "Diploma");
        }

        // GET: /Account/Logout
        public IActionResult Logout()
        {
            return RedirectToAction("Index", "Diploma");
        }
    }
}
