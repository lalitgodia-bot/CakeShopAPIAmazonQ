using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CakeShopAPIAmazonQ.Models;
using CakeShopAPIAmazonQ.Services;

namespace CakeShopAPIAmazonQ.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(User user)
    {
        var created = await _userService.CreateAsync(user);
        return CreatedAtAction(nameof(GetUser), new { id = created.UserId }, created);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateUser(int id, User user)
    {
        var updated = await _userService.UpdateAsync(id, user);
        if (updated == null) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var deleted = await _userService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}