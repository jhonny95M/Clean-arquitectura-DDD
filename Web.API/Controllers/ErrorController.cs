using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.API.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/error")]
    public IActionResult Error()
    {
        Exception? exception=HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        return Problem();
    }
    }
