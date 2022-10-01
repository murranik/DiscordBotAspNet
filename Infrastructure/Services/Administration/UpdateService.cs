using Infrastructure.Database;
using Interfaces.Administration;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Infrastructure.Services.Administration;

public class UpdateService : IUpdateService
{
	private readonly DiscordBotContext _context;

	public UpdateService(DiscordBotContext context)
	{
		_context = context;
	}

	public async Task ChangeUserPrestigeLevelAsync(string userId, int newLevel)
	{
		var user = _context.Users.FirstOrDefault(x => x.DiscordId == userId);

		if (user == null) return;

		user.PrestigeLevel = newLevel;
		_context.Users.Update(user);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateModelAsync<T>(T data, Type type) where T : class
	{
		var options = new JsonSerializerOptions()
		{
			Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
			PropertyNameCaseInsensitive = false,
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			WriteIndented = true
		};

		var json = JsonSerializer.Serialize(data, options);

		var res = JsonSerializer.Deserialize(json, type, options);
		//await _context.UpdateAsync(res, res.GetType().GetProperty("Id").GetValue(res), type);
		await _context.SaveChangesAsync();
	}

}