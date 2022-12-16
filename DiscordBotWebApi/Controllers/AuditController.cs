using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Firestore;

namespace DiscordBotWebApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuditController : Controller
	{
		private readonly IAuditService _auditService;

		public AuditController(IAuditService auditService)
		{
			_auditService = auditService;
		}

		[HttpGet("Commands/{guildId}")]
		public async Task<IActionResult> GetCommandsHistory([FromQuery] int? rowsCount, ulong guildId)
		{
			var res = await _auditService.GetCommandsHistory(rowsCount, guildId);
			return Ok(res);
		}
	}
}
