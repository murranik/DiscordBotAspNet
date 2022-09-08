using Discord.WebSocket;
using Interfaces;
using Models;

namespace DiscordBotWebApi.Bot.Handlers
{
	public class OnRoleUpdateHandler
	{
		private readonly IUserService _userManager;

		public OnRoleUpdateHandler(IUserService userManager)
		{
			_userManager = userManager;
		}

		public async Task Handler(SocketRole oldRole, SocketRole newRole)
		{
			await _userManager.UpdateRole(
				new DiscordRole()
				{
					DiscordId = newRole.Id,
					Name = newRole.Name
				}
			);
		}
	}
}
