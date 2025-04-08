using ArchBackend.Core.Models;
using ArchBackend.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ArchBackend.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetCategoryAsync();
            return View(categories);
        }

        [HttpGet("GetCategory/{id}")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetCategoryAsync();
            return Json(categories);
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryIdAsync(id);
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] Category category)
        {
            var createdCategory = await _categoryService.AddCategoryAsync(category);
            return Ok(createdCategory);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category category)
        {
            if (id != category.Id)
                return BadRequest("Id mismatch");
            
            var updatedCategory = await _categoryService.UpdateCategoryAsync(category);
            return Ok(updatedCategory);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var deletedCategory = await _categoryService.DeleteCategoryAsync(id);
            return Ok(deletedCategory);
        }

    }
}
