using Microsoft.AspNetCore.Mvc;

namespace DiscordBotWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatusController : Controller
{
	[HttpGet]
	public IActionResult GetStatus()
	{
		return Ok("Server is currently online");
	}
}