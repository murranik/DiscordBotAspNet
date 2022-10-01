using ThemeWebApi.Database.Models;

namespace ThemeWebApi.Interfaces;

public interface IThemesService
{
	public Task AddThemeAsync(ThemeData themeData);
	public ThemeData GetThemeByName(string name);
	public List<ThemeData> GetThemes(int? count);
	public Task Delete(string name);
}