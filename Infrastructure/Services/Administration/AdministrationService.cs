using DTOModels;
using Infrastructure.Database;
using Interfaces.Administration;
using Models;

namespace Infrastructure.Services.Administration;

public class AdministrationService : IAdministrationService
{
	private readonly DiscordBotContext _discordBotContext;

	public AdministrationService(DiscordBotContext discordBotContext) 
	{
		_discordBotContext = discordBotContext;
	}

	public bool Login(AdministratorDto administrator) 
	{
		if (administrator.Password.Length > 8 && administrator.Password.Length <= 12) 
		{
			return true;
		}

		return false;
	}

	public async Task UpsertAdministratorAsync(Administrator administrator) 
	{
		if(!_discordBotContext.Administrators.Any(x => x.Id == administrator.Id))
			await _discordBotContext.Administrators.AddAsync(administrator);
		else
			_discordBotContext.Administrators.Update(administrator);
		await _discordBotContext.SaveChangesAsync();
	}

	public List<Administrator> GetAdministrators()
	{
		return _discordBotContext.Administrators.ToList();
	}

	public async Task<Administrator> FindAdministratorAsync(string id) 
	{
		return await _discordBotContext.Administrators.FindAsync(id);
	}

	public bool IsExistEmail(string administratorEmail)
	{
		return _discordBotContext.Administrators.Any(x => x.Email == administratorEmail);
	}
}