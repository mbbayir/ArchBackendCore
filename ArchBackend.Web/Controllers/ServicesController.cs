using ArchBackend.Core.Models;
using ArchBackend.Core.Services;
using ArchBackend.Service.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize(Roles = "Admin")]

[Route("Services")]
public class ServicesController : Controller
{

    private readonly IOurService _ourService;
    private readonly IMapper _mapper;


    public ServicesController(IOurService ourService, IMapper mapper)
    {
        _ourService = ourService;
        _mapper = mapper;
    }

    [HttpGet("Index")]
    public async Task<IActionResult> Index()
    {
        var services = await _ourService.GetOurServiceAsync();
        return View(services);
    }

    [HttpGet("GetOurService/{id}")]
    public async Task<IActionResult> GetOurServices()
    {
        var services = await _ourService.GetOurServiceAsync();
        return Json(services);
    }

    [HttpGet("GetOurServiceById/{id}")]
    public async Task<IActionResult> GetOurServiceById(int id)
    {
        var service = await _ourService.GetOurServiceIdAsync(id);
        if (service == null)
            return NotFound();

        return Ok(service);
    }



    [HttpPost("AddOurServiceAsync")]
    public async Task<IActionResult> AddOurServiceAsync([FromBody] ArchBackend.Core.Models.OurService ourService)
    {
        var createdService = await _ourService.AddOurServiceAsync(ourService);
        return Ok(createdService);
    }


    [HttpPut("UpdateServiceAsync")]
    public async Task<IActionResult> UpdateServiceAsync([FromBody] ArchBackend.Core.Models.OurService ourService)
    {
        var updatedService = await _ourService.UpdateOurServiceAsync(ourService);
        return Ok(updatedService);
    }


    [HttpDelete("DeleteOurServiceAsync/{id}")]
    public async Task<IActionResult> DeleteOurServiceAsync(int id)
    {
        var deletedOurService = await _ourService.DeleteOurServiceAsync(id);

        if (deletedOurService)
        {
            return Ok(new { message = "Service deleted successfully." });
        }

        return NotFound(new { message = "Service not found." });
    }


}