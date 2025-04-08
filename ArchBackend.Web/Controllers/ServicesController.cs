using ArchBackend.Core.Models;
using ArchBackend.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet("GetServices")]
    public async Task<IActionResult> GetServices()
    {
        var services = await _ourService.GetOurServiceAsync();
        return Json(services);
    }

    [HttpPost("AddOurServiceAsync")]
    public async Task<IActionResult> AddOurServiceAsync([FromBody] OurService ourService)
    {
        var createdOurservice = await _ourService.AddOurServiceAsync(ourService);
        return Ok(createdOurservice);
    }
}