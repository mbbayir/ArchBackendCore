using ArchBackend.Core.Services;
using ArchBackend.Repository.Models;
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
            var projects = await _projectService.GetProjects();
            return Ok(projects);
        }

        [HttpGet]
        public async Task<IActionResult> GetProjectById(int id)
        {
            var project = await _projectService.GetProjectById(id);
            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(Project project)
        {
            var createdProject= await _projectService.CreateProject(project);
            return Ok(createdProject);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProject(Project project)
        {
            var updatedProject = await _projectService.UpdateProject(project);
            return Ok(updatedProject);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var result = await _projectService.DeleteProject(id);
            return Ok(result);
        }
    }
}
