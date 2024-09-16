using System.Security.Claims;

namespace API.Extensions;

/**
 * This class is an extension of the ClaimsPrincipal class
 * We are adding a new method to the ClaimsPrincipal class
 */

public static class ClaimsPrincipleExtensions
{
    public static string GetUsername(this ClaimsPrincipal user)
    {
        var username = user.FindFirstValue(ClaimTypes.NameIdentifier) 
                       ?? throw new Exception("Cannot get username from token");
        return username;
    }
}