using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CakeShopAPIAmazonQ.Models;
using CakeShopAPIAmazonQ.Services;

namespace CakeShopAPIAmazonQ.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderItemsController : ControllerBase
{
    private readonly IOrderItemService _orderItemService;

    public OrderItemsController(IOrderItemService orderItemService)
    {
        _orderItemService = orderItemService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderItem>>> GetOrderItems()
    {
        var orderItems = await _orderItemService.GetAllAsync();
        return Ok(orderItems);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderItem>> GetOrderItem(int id)
    {
        var orderItem = await _orderItemService.GetByIdAsync(id);
        if (orderItem == null) return NotFound();
        return Ok(orderItem);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<OrderItem>> CreateOrderItem(OrderItem orderItem)
    {
        var created = await _orderItemService.CreateAsync(orderItem);
        return CreatedAtAction(nameof(GetOrderItem), new { id = created.OrderItemId }, created);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateOrderItem(int id, OrderItem orderItem)
    {
        var updated = await _orderItemService.UpdateAsync(id, orderItem);
        if (updated == null) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteOrderItem(int id)
    {
        var deleted = await _orderItemService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}