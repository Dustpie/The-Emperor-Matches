using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/**
 * ControllerBase is a class that has all the functionality of a controller
 * We can use ControllerBase instead of Controller
 * ControllerBase is a more lightweight version of Controller
 */
[Authorize]

public class UsersController(IUserRepository userRepository, IMapper mapper) : BaseApiController // Primary Constructor
{
    [AllowAnonymous]
    [HttpGet] // We can only have one of each Request Type per controller
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        var users = await userRepository.GetUsersAsync();
        var usersToReturn = mapper.Map<IEnumerable<MemberDto>>(users);
        // return NotFound(users); --> We can directly return an HTTP response containing the list
        return Ok(usersToReturn); // Returning the users variable creates automatically an "Ok" HTTP request --> type safety is not good
    }

    [HttpGet("{username}")] // /api/users/3 --> Brackets are needed for the dynamic ID of the users and :int is for type safety
    public async Task<ActionResult<MemberDto>> GetUserByName(string username)
    {
        var user = await userRepository.GetUserByUsernameAsync(username);
        if (user == null) return NotFound();

        return mapper.Map<MemberDto>(user);
    }
}