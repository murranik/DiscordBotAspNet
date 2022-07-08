using DTOModels;
using Infrastructure.Services.Administration;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace DiscordBotWebApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthController : Controller
	{
		private readonly AuthService _authService;

		public AuthController(AuthService authService) 
		{
			_authService = authService;
		}

		[HttpPost("Register")]
		public async Task<IActionResult> Register([FromBody]AdministratorDto administrator) 
		{
			var res = await _authService.Register(administrator);

			if(res == string.Empty)
				return Ok();
			return BadRequest(res);
		}

		[HttpGet("ConfirmEmail")]
		public async Task<IActionResult> ConfirmEmail([FromQuery]ConfimationEmail query)
		{
			await _authService.ConfirmEmail(query.AdministratorId, query.Token);

			return Ok();
		}
	}

	public class ConfimationEmail
	{
		public string AdministratorId { get; set; }
		public string Token { get; set; }
		
	}
}
