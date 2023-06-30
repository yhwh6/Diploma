using Microsoft.AspNetCore.Mvc;
using Diploma.Models;

namespace Diploma.Controllers
{
    public class HomeController : Controller
    {
        private readonly DiplomaContext _context;

        public HomeController(DiplomaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Получение информации о проекте, услугах, статьях и контактах из базы данных
            var project = _context.Projects.FirstOrDefault();
            var services = _context.Services.ToList();
            var blogs = _context.Blogs.ToList();
            var contacts = _context.Users.FirstOrDefault();

            // Передача данных в представление
            var viewModel = new HomeViewModel
            {
                Projects = project,
                Services = services,
                Blogs = blogs,
                Contacts = contacts
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SubmitApplication(Application application)
        {
            if (ModelState.IsValid)
            {
                // Сохранение заявки в базе данных
                _context.Applications.Add(application);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            // В случае ошибки валидации возвращаем на главную страницу с сообщением об ошибке
            return RedirectToAction("Index", new { error = "Invalid application data" });
        }
    }
}
