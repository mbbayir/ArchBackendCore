using ArchBackend.Core.Models;
using ArchBackend.Core.Repositories;
using ArchBackend.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace ArchBackend.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ContactController : Controller
    {
        private readonly IContactMessageService _contactMessageService;

        public ContactController(IContactMessageService contactMessageService)
        {
            _contactMessageService = contactMessageService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var messages = await _contactMessageService.GetAllMessages();
            return View(messages);
        }


        [HttpPost]
        public async Task<IActionResult> Contact(ContactMessage model)
        {
                await _contactMessageService.CreateMessage(model);

                return Json(new {success = true});
            
        }
    }
}
