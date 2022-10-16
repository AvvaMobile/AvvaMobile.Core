using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AvvaMobile.Core.APIControllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class ApiBaseController : ControllerBase
    {

    }
}