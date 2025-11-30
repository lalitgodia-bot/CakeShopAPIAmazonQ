using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CakeShopAPIAmazonQ.Models;
using CakeShopAPIAmazonQ.Services;

namespace CakeShopAPIAmazonQ.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartsController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartsController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cart>>> GetCarts()
    {
        var carts = await _cartService.GetAllAsync();
        return Ok(carts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Cart>> GetCart(int id)
    {
        var cart = await _cartService.GetByIdAsync(id);
        if (cart == null) return NotFound();
        return Ok(cart);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Cart>> CreateCart(Cart cart)
    {
        var created = await _cartService.CreateAsync(cart);
        return CreatedAtAction(nameof(GetCart), new { id = created.CartId }, created);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateCart(int id, Cart cart)
    {
        var updated = await _cartService.UpdateAsync(id, cart);
        if (updated == null) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteCart(int id)
    {
        var deleted = await _cartService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}