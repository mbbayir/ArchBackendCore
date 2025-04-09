using ArchBackend.Core.Models;
using ArchBackend.Core.Services;
using ArchBackend.Service.Services;
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
    public async Task<IActionResult> AddOurServiceAsync([FromBody] ArchBackend.Core.Models.OurService ourService)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var createdOurService = await _ourService.AddOurServiceAsync(ourService);
            return Ok(createdOurService);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("UpdateOurServiceAsync")]
    public async Task<IActionResult> UpdateOurServiceAsync(int id , [FromBody] ArchBackend.Core.Models.OurService ourService)
    {
        if (id != ourService.Id)
            return BadRequest("Bad Request");
        var updatedOurService = _ourService.UpdateOurServiceAsync(ourService);
        return Ok(updatedOurService);
    }




    [HttpDelete("{id}")]
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