using Interfaces.Administration;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace DiscordBotWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UpdateController : ControllerBase
{

	private readonly IUpdateService _updateService;

	public UpdateController(IUpdateService updateService)
	{
		_updateService = updateService;
	}

	[HttpPost("{type}")]
	public async Task<IActionResult> UpdateModelAsync([FromBody] object data, string type)
	{
		Type resType = null;
		foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
		{
			resType = a.GetTypes().FirstOrDefault(x => x.Name.ToLower() == type.ToLower());
			if (resType is not null)
				break;
		}

		if (resType is not null)
		{
			await _updateService.UpdateModelAsync(data, resType);
			return Ok();
		}
		else
		{
			return BadRequest("Bad type");
		}
	}

	[HttpPost("ChangeUserPrestigeLevel")]
	public async Task<IActionResult> ChangeUserPrestigeLevelAsync(string userDiscordId, int newLevel)
	{
		await _updateService.ChangeUserPrestigeLevelAsync(userDiscordId, newLevel);
		return Ok();
	}
}