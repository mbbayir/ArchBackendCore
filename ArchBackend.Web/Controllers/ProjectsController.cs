    using ArchBackend.Core.Services;
using ArchBackend.Core.Models;
using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ArchBackend.Web.Models;

namespace ArchBackend.Web.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public ProjectsController(IProjectService projectService, IMapper mapper)
        {
            _projectService = projectService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var projects = await _projectService.GetProjectsAsync();
            return View(projects);

        }


        [HttpGet("GetProjects")]
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
        public async Task<IActionResult> AddProject([FromForm] Project project, [FromForm] IFormFile formFile)
        {

            var createdProject = await _projectService.AddProjectAsync(project, formFile);
            return Ok(createdProject);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateProject(int id, [FromForm] Project project, [FromForm] IFormFile formFile)
        {
          
            var updatedProject = await _projectService.UpdateProjectAsync(project, formFile);
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
