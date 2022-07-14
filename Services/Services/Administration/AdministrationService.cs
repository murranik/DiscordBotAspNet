using Infrastructure.Database;
using Models;

namespace Infrastructure.Services.Administration
{
	public class AdministrationService
	{
		private readonly DiscordBotContext _discordBotContext;

		public AdministrationService(DiscordBotContext discordBotContext) 
		{
			_discordBotContext = discordBotContext;
		}

		public async Task UpsertAdministratorAsync(Administrator administrator) 
		{
			if(!_discordBotContext.Administrators.Any(x => x.Id == administrator.Id))
				await _discordBotContext.Administrators.AddAsync(administrator);
			else
				_discordBotContext.Administrators.Update(administrator);
			await _discordBotContext.SaveChangesAsync();
		}

		public async Task<Administrator> FindAdministratorAsyns(string id) 
		{
			return await _discordBotContext.Administrators.FindAsync(id);
		}

		public bool IsExistEmail(string administratorEmail)
		{
			return _discordBotContext.Administrators.Any(x => x.Email == administratorEmail);
		}
	}
}
