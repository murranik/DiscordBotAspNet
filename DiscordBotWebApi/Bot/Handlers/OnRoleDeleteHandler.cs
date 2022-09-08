using Discord.WebSocket;
using Interfaces;

namespace DiscordBotWebApi.Bot.Handlers
{
	public class OnRoleDeleteHandler
	{
		private readonly IUserService _userManager;

		public OnRoleDeleteHandler(IUserService userManager)
		{
			_userManager = userManager;
		}

		public async Task Handler(SocketRole role)
		{
			await _userManager.RemoveRole(role.Id);
		}
	}
}
