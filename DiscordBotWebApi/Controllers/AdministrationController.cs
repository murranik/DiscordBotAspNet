using DTOModels;
using Interfaces.Administration;
using Microsoft.AspNetCore.Mvc;

namespace DiscordBotWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdministrationController : ControllerBase
{
	private readonly IAdministrationService _administrationService;

	public AdministrationController(IAdministrationService administrationService)
	{
		_administrationService = administrationService;
	}

	[HttpPost]
	public IActionResult Login(AdministratorDto administrator) 
	{
		if(_administrationService.Login(administrator))
			return Ok();

		return BadRequest("Not valid password");
	}
}