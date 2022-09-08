using Microsoft.AspNetCore.Mvc;
using ThemeWebApi.Database.Models;
using ThemeWebApi.Interfaces;

namespace ThemeWebApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ThemesController : ControllerBase
	{
		private readonly IThemesService _themesSevrice;

		public ThemesController(IThemesService themesSevrice)
		{
			_themesSevrice = themesSevrice;
		}

		[HttpPost("Add")]
		public async Task<ActionResult> Add([FromBody] ThemeData theme)
		{
			await _themesSevrice.AddThemeAsync(theme);
			return Ok();
		}

		[HttpPut("Edit")]
		public ActionResult Edit([FromBody] ThemeData theme)
		{
			return Ok();
		}

		[HttpGet]
		public ActionResult GetAll([FromQuery]int? count)
		{
			var themes = _themesSevrice.GetThemes(count);
			return Ok(themes);
		}

		[HttpGet("{name}")]
		public ActionResult GetByName(string name)
		{
			var theme = _themesSevrice.GetThemeByName(name);
			return Ok(theme);
		}

		[HttpDelete("Delete/{name}")]
		public async Task<ActionResult> Delete(string name)
		{
			await _themesSevrice.Delete(name);
			return Ok();
		}
	}
}
