using Microsoft.AspNetCore.Mvc;

namespace ArchBackend.Web.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
