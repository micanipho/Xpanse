using Microsoft.AspNetCore.Mvc;
using server.Dtos;
using server.Services.Interfaces;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategory categoryService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<CategoryResponse>>> GetAllCategories()
        {
            var categories = await categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryResponse>> GetCategoryById(int id)
        {
            var category = await categoryService.GetCategoryByIdAsync(id);
            if (category is null) return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryResponse>> CreateCategory(CreateCategoryRequest category)
        {
            var created = await categoryService.CreateCategoryAsync(category);
            return CreatedAtAction(nameof(GetCategoryById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CreateCategoryRequest category)
        {
            var updated = await categoryService.UpdateCategoryAsync(id, category);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var deleted = await categoryService.DeleteCategoryAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
