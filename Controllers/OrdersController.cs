using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CakeShopAPIAmazonQ.Models;
using CakeShopAPIAmazonQ.Services;

namespace CakeShopAPIAmazonQ.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
    {
        var orders = await _orderService.GetAllAsync();
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrder(int id)
    {
        var order = await _orderService.GetByIdAsync(id);
        if (order == null) return NotFound();
        return Ok(order);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Order>> CreateOrder(Order order)
    {
        var created = await _orderService.CreateAsync(order);
        return CreatedAtAction(nameof(GetOrder), new { id = created.OrderId }, created);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateOrder(int id, Order order)
    {
        var updated = await _orderService.UpdateAsync(id, order);
        if (updated == null) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var deleted = await _orderService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}