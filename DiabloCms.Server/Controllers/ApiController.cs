using Microsoft.AspNetCore.Mvc;

namespace DiabloCms.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiController : ControllerBase
    {
    }
}