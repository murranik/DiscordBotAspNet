using Infrastructure.Services;
using Infrastructure.Services.Administration;
using Microsoft.AspNetCore.Mvc;

namespace DiscordBotWebApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class IsExistController : Controller
	{
		private readonly AdministrationService _administrationService;

		public IsExistController(AdministrationService administrationService)
		{
			_administrationService = administrationService;
		}

		[HttpGet]
		public IActionResult CheckIsAdministratorExist(string email)
		{
			return Ok(_administrationService.IsExistEmail(email));
		}
	}
}
