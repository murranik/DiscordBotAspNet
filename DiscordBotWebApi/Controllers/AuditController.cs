using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Firestore;

namespace DiscordBotWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuditController : Controller
{
	private readonly IAuditService _auditService;

	public AuditController(IAuditService auditService)
	{
		_auditService = auditService;
	}

	[HttpGet("Errors/{guildId}")]
	public async Task<IActionResult> GetErrorHistory([FromQuery]int? rowsCount, ulong guildId) 
	{
		var res = await _auditService.GetErrorsAsync(rowsCount, guildId);
		return Ok(res);
	}

	[HttpGet("Commands/{guildId}")]
	public async Task<IActionResult> GetCommandHistory([FromQuery] int? rowsCount, ulong guildId)
	{
		var res = await _auditService.GetCommandHistory(rowsCount, guildId);
		return Ok(res);
	}
}