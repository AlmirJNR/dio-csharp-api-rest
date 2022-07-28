using Microsoft.AspNetCore.Mvc;

namespace CsharpApiRest.Controllers;

[ApiController]
[Route("api/v1")]
public class ApiController : ControllerBase
{
    /// <summary>
    ///     Returns a default API response
    /// </summary>
    /// <response code="200">Returns the default api response</response>
    [HttpGet]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get() => Ok("Hello, Welcome To Almir Junior's Series API");
}
