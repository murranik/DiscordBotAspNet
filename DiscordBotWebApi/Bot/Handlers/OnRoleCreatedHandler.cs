using Discord.WebSocket;
using Interfaces;
using Models;

namespace DiscordBotWebApi.Bot.Handlers
{
	public class OnRoleCreatedHandler
	{
		private readonly IUserService _userManager;

		public OnRoleCreatedHandler(IUserService userManager)
		{
			_userManager = userManager;
		}

		public async Task Handler(SocketRole role)
		{
			await _userManager.AddRole(
				new DiscordRole()
				{
					DiscordId = role.Id,
					Name = role.Name
				}
			);
		}
	}
}
