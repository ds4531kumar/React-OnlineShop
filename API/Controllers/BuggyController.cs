using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class BuggyController : BaseApiController
  {
    [HttpGet("not-found")]
    public IActionResult GetNotFound()
    {
      return NotFound("This is not found");
    }

    [HttpGet("bad-request")]
    public IActionResult GetBadRequest()
    {
      return BadRequest("This is not a good request");
    }

    [HttpGet("unauthorized")]
    public IActionResult GetUnauthorized()
    {
      return Unauthorized("This is not authorized");
    }

    [HttpGet("validation-error")]
    public IActionResult GetValidationError()
    {
      ModelState.AddModelError("Problem 1", "This is first validation error");

      ModelState.AddModelError("Problem 2", "This is second validation error");

      return ValidationProblem();
    }
    [HttpGet("server-error")]
    public IActionResult GetServerError()
    {
      throw new Exception("This is a server error");
    }



  }
}