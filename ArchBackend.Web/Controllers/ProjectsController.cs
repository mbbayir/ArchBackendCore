    using ArchBackend.Core.Services;
using ArchBackend.Core.Models;
using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ArchBackend.Web.Models;
using System.Runtime.InteropServices;
using ArchBackend.Core.Models.Bridges;
using Microsoft.AspNetCore.Authorization;

namespace ArchBackend.Web.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly ICategoryService _categoryService;
        private readonly IOurService _ourService;
        private readonly IMapper _mapper;

        public ProjectsController(IProjectService projectService, ICategoryService categoryService, IOurService ourService, IMapper mapper)
        {
            _projectService = projectService;
            _categoryService = categoryService;
            _ourService = ourService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
          var categories = await _categoryService.GetCategoryAsync();
          ViewBag.Categories = categories;

          var ourServices = await _ourService.GetOurServiceAsync();
          ViewBag.OurServices = ourServices;
          return View();
        }


        [HttpGet]
        public async Task<JsonResult> GetProctWithCategory()
        {
            var projects = await _projectService.DetailWithCategory();
            return Json(projects);
        }


        public async Task<JsonResult> GetProctWithOurService()
        {
            var projects = await _projectService.DetailWithOurService();
            return Json(projects);
        }


        [HttpGet]
        public async Task<IActionResult> GetProjectById(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);

            return Ok(project);
        }


        [HttpPost]
        public async Task<IActionResult> AddProject([FromForm] Project project, [FromForm] IFormFile formFile, [FromForm] List<int> CategoryIds , [FromForm] List<int> OurServiceIds)
        {
            project.ProjectCategories = CategoryIds.Select(cid => new ProjectCategory { CategoryId = cid }).ToList();
            project.OurServiceProjects = OurServiceIds
                .Select(oid => new OurServiceProject { OurServiceId = oid })
                .ToList();

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
