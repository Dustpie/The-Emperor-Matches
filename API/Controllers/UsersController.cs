using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")] // /api/users
public class UsersController(DataContext context) : ControllerBase // Primary Constructor
{
    /*
     * Old way Constructor
     * private readonly DataContext context
     *
     * public UsersController(DataContext context)
     * {
     * this.context = context; or _context = context;
     * }
     */

    [HttpGet] // We can only have one of each Request Type per controller
    public async Task <ActionResult<IEnumerable<User>>> GetUsers() // We make the request async, so one request doesn't block others.
    {
        var users = await context.Users.ToListAsync();
        // return NotFound(users); --> We can directly return an HTTP response containing the list
        return users; // Automatically creates an "Ok" HTTP request
    }

    [HttpGet("{id:int}")] // /api/users/3 --> Brackets are needed for the dynamic ID of the users and :int is for type safety
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null) return NotFound();

        return user;
    }
}