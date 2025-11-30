using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CakeShopAPIAmazonQ.Models;
using CakeShopAPIAmazonQ.Services;

namespace CakeShopAPIAmazonQ.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
        var categories = await _categoryService.GetAllAsync();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategory(int id)
    {
        var category = await _categoryService.GetByIdAsync(id);
        if (category == null) return NotFound();
        return Ok(category);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Category>> CreateCategory(Category category)
    {
        var created = await _categoryService.CreateAsync(category);
        return CreatedAtAction(nameof(GetCategory), new { id = created.CategoryId }, created);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateCategory(int id, Category category)
    {
        var updated = await _categoryService.UpdateAsync(id, category);
        if (updated == null) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var deleted = await _categoryService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}