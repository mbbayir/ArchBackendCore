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
        private readonly IProjectService _projectService;

        public HomeController(ILogger<HomeController> logger, IContactMessageService contactMessageService , IProjectService projectService)
        {
            _logger = logger;
            _contactMessageService = contactMessageService;
            _projectService = projectService; 
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
        public async Task<IActionResult> Projects()
        {
            var projects = await _projectService.GetProjectsAsync();
            return View(projects);
        }

        [Route ("project/detail/{id}")]
        public async Task<IActionResult> ProjectDetails(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
                return NotFound(); 

            return View(project);
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
