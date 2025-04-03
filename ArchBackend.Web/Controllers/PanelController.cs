using Microsoft.AspNetCore.Mvc;

namespace ArchBackend.Web.Controllers
{
    public class PanelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
