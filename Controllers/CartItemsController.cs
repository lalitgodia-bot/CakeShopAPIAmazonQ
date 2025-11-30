using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CakeShopAPIAmazonQ.Models;
using CakeShopAPIAmazonQ.Services;

namespace CakeShopAPIAmazonQ.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartItemsController : ControllerBase
{
    private readonly ICartItemService _cartItemService;

    public CartItemsController(ICartItemService cartItemService)
    {
        _cartItemService = cartItemService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CartItem>>> GetCartItems()
    {
        var cartItems = await _cartItemService.GetAllAsync();
        return Ok(cartItems);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CartItem>> GetCartItem(int id)
    {
        var cartItem = await _cartItemService.GetByIdAsync(id);
        if (cartItem == null) return NotFound();
        return Ok(cartItem);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<CartItem>> CreateCartItem(CartItem cartItem)
    {
        var created = await _cartItemService.CreateAsync(cartItem);
        return CreatedAtAction(nameof(GetCartItem), new { id = created.CartItemId }, created);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateCartItem(int id, CartItem cartItem)
    {
        var updated = await _cartItemService.UpdateAsync(id, cartItem);
        if (updated == null) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteCartItem(int id)
    {
        var deleted = await _cartItemService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}