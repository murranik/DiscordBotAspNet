using Microsoft.EntityFrameworkCore;
using ThemeWebApi.Database;
using ThemeWebApi.Database.Models;
using ThemeWebApi.Interfaces;

namespace ThemeWebApi.Services;

public class ThemesSevrice : IThemesService
{
	private readonly ThemeDbContext _context;

	public ThemesSevrice(ThemeDbContext context)
	{
		_context = context;
	}

	public async Task AddThemeAsync(ThemeData themeData) {
		if (!_context.Themes.Any(x => x.Name == themeData.Name)) {
			await _context.Themes.AddAsync(themeData);
			await _context.SaveChangesAsync();
		}
	}

	public ThemeData GetThemeByName(string name)
	{
		return _context.Themes
			.AsNoTracking()
			.Include(x => x.DataTableCellColors)
			.Include(x => x.DropdownButtonColors)
			.Include(x => x.FloatingBoxColors)
			.FirstOrDefault(x => x.Name == name);
	}

	public List<ThemeData> GetThemes(int? count)
	{
		return count == null
			? _context.Themes
				.AsNoTracking()
				.Include(x => x.DataTableCellColors)
				.Include(x => x.DropdownButtonColors)
				.Include(x => x.FloatingBoxColors)
				.ToList()
			: _context.Themes
				.OrderBy(x => x.Name)
				.Take((int)count)
				.AsNoTracking()
				.Include(x => x.DataTableCellColors)
				.Include(x => x.DropdownButtonColors)
				.Include(x => x.FloatingBoxColors)
				.ToList();
	}

	public async Task Delete(string name)
	{
		var themeData = await _context.Themes
			.AsNoTracking()
			.Include(x => x.DataTableCellColors)
			.Include(x => x.DropdownButtonColors)
			.Include(x => x.FloatingBoxColors)
			.FirstOrDefaultAsync(x => x.Name == name);
		_context.Themes.Remove(themeData);
		await _context.SaveChangesAsync();
	}
}