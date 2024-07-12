using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

/**
 * ControllerBase is a class that has all the functionality of a controller
 * We can use ControllerBase instead of Controller
 * ControllerBase is a more lightweight version of Controller
 */
public class UsersController(DataContext context) : BaseApiController // Primary Constructor
{
    [HttpGet] // We can only have one of each Request Type per controller
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        var users = await context.Users.ToListAsync();
        // return NotFound(users); --> We can directly return an HTTP response containing the list
        return Ok(users); // Automatically creates an "Ok" HTTP request
    }

    [HttpGet("{id:int}")] // /api/users/3 --> Brackets are needed for the dynamic ID of the users and :int is for type safety
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null) return NotFound();

        return Ok(user);
    }
}