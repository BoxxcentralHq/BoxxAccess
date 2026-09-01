using BoxxAccess.Contracts.Health;
using Microsoft.AspNetCore.Mvc;

namespace BoxxAccess.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class HealthController : ControllerBase
{
    [HttpGet]
    public ActionResult<HealthResponse> Get() => Ok(new HealthResponse("Healthy", DateTimeOffset.UtcNow));
}
