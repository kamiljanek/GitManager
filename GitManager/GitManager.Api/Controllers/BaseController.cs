using Microsoft.AspNetCore.Mvc;

namespace GitManagerApi.Controllers;

[ApiController]
[Route("/v{version:apiVersion}/[controller]")]
public class BaseController : ControllerBase
{
}