using Microsoft.AspNetCore.Mvc;

namespace Diploma.Controllers
{
    public class ContactsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
