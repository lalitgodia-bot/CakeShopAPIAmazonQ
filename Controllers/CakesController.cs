using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CakeShopAPIAmazonQ.Models;
using CakeShopAPIAmazonQ.Services;

namespace CakeShopAPIAmazonQ.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CakesController : ControllerBase
{
    private readonly ICakeService _cakeService;

    public CakesController(ICakeService cakeService)
    {
        _cakeService = cakeService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cake>>> GetCakes()
    {
        var cakes = await _cakeService.GetAllAsync();
        return Ok(cakes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Cake>> GetCake(int id)
    {
        var cake = await _cakeService.GetByIdAsync(id);
        if (cake == null) return NotFound();
        return Ok(cake);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Cake>> CreateCake(Cake cake)
    {
        var created = await _cakeService.CreateAsync(cake);
        return CreatedAtAction(nameof(GetCake), new { id = created.CakeId }, created);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateCake(int id, Cake cake)
    {
        var updated = await _cakeService.UpdateAsync(id, cake);
        if (updated == null) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteCake(int id)
    {
        var deleted = await _cakeService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}