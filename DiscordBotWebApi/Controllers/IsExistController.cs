using Interfaces.Administration;
using Microsoft.AspNetCore.Mvc;

namespace DiscordBotWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IsExistController : Controller
{
	private readonly IAdministrationService _administrationService;

	public IsExistController(IAdministrationService administrationService)
	{
		_administrationService = administrationService;
	}

	[HttpGet]
	public IActionResult CheckIsAdministratorExist(string email)
	{
		return Ok(_administrationService.IsExistEmail(email));
	}
}