using System.Diagnostics;
using ArchBackend.Core.Models;
using ArchBackend.Core.Services;
using ArchBackend.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ArchBackend.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IContactMessageService _contactMessageService;

        public HomeController(ILogger<HomeController> logger, IContactMessageService contactMessageService)
        {
            _logger = logger;
            _contactMessageService = contactMessageService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Services()
        {
            return View();
        }
        public IActionResult Projects()
        {
            return View();
        }

        public IActionResult ProjectGalery()
        {
            return View();
        }
        public IActionResult WorkProcess()
        {
            return View();
        }

        public IActionResult FAQ()
        {
            return View();
        }

        public IActionResult Blog()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(ContactMessage model)
        {
            await _contactMessageService.CreateMessage(model);
            return RedirectToAction("Contact"); // formdan sonra tekrar Contact sayfas?na dön
        }

        public IActionResult Post()
        {
            return View();
        }

        public IActionResult Post2()
        {
            return View();
        }
        public IActionResult Post3()
        {
            return View();
        }
        public IActionResult Post4()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
