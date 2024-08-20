using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/**
 * ControllerBase is a class that has all the functionality of a controller
 * We can use ControllerBase instead of Controller
 * ControllerBase is a more lightweight version of Controller
 */
[Authorize]

public class UsersController(IUserRepository userRepository) : BaseApiController // Primary Constructor
{
    [AllowAnonymous]
    [HttpGet] // We can only have one of each Request Type per controller
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        var users = await userRepository.GetMembersAsync(); // We are getting the users from the database
     
        return Ok(users); // Returning the users variable creates automatically an "Ok" HTTP request --> type safety is not good
    }

    [HttpGet("{username}")] // /api/users/3 --> Brackets are needed for the dynamic ID of the users and :int is for type safety
    public async Task<ActionResult<MemberDto>> GetUserByName(string username)
    {
        var user = await userRepository.GetMemberAsync(username);
        if (user == null) return NotFound();

        return Ok(user);
    }
}