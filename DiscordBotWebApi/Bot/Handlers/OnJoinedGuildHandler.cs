using Discord.WebSocket;
using Interfaces;
using Interfaces.Administration;
using NickBuhro.Translit;

namespace DiscordBotWebApi.Bot.Handlers
{
	public class OnJoinedGuildHandler
	{
		private readonly IAdministrationService _administrationService;
		private readonly ITokenService _tokenService;
		public OnJoinedGuildHandler(IAdministrationService administrationService, ITokenService tokenService)
		{
			_administrationService = administrationService;
			_tokenService = tokenService;
		}

		public async Task Handler(SocketGuild socketGuild)
		{
			var emailName = Transliteration.CyrillicToLatin(socketGuild.Owner.DisplayName, Language.Ukrainian);
			await _administrationService.UpsertAdministratorAsync(new Models.Administrator() { 
				ConfirmEmail = true,
				Email = $"{emailName}@shiki.ui",
				GuildId = socketGuild.Id,
				Id = Guid.NewGuid().ToString(),
				Nickname = socketGuild.Owner.DisplayName,
				Password = _tokenService.Encrypt(socketGuild.OwnerId.ToString()).Substring(0, 9) + "Sa1",
			});
		}
	}
}
