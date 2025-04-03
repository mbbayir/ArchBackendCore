using ArchBackend.Core.Services;
using ArchBackend.Repository.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArchBackend.Web.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var projects = await _projectService.GetProjectsAsync();
            return Ok(projects);
        }

        [HttpGet]
        public async Task<IActionResult> GetProjectById(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> AddProject([FromForm]Project project ,[FromForm] IFormFile formFile)
        {
            Console.WriteLine("Controller methodu çağrıldı.");
            Console.WriteLine($"Project Name: {project?.Name}");
            Console.WriteLine($"Project Description: {project?.Description}");
            Console.WriteLine($"Project Tag: {project?.Tag}");
            var createdProject= await _projectService.AddProjectAsync(project, formFile);
            return Ok(createdProject);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProject(Project project)
        {
            var updatedProject = await _projectService.UpdateProjectAsync(project);
            return Ok(updatedProject);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var result = await _projectService.DeleteProjectAsync(id);
            return Ok(result);
        }
    }
}
