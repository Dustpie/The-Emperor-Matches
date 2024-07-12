using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/**
 * ControllerBase is a class that provides a base class for an MVC controller without view support.
 */
[ApiController]
[Route("[controller]")]
public class BaseController : ControllerBase
{
    
}