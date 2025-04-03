using Microsoft.AspNetCore.Mvc;

namespace ArchBackend.Web.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
