using Discord.WebSocket;
using Interfaces;

namespace DiscordBotWebApi.Bot.Handlers;

public class OnMemberLeftHandler
{
	private readonly DiscordSocketClient _client;
	private readonly IUserService _userService;

	public OnMemberLeftHandler(DiscordSocketClient client, IUserService userService)
	{
		_client = client;
		_userService = userService;
	}

	public async Task Handler(SocketGuild guild, SocketUser user)
	{
		if (_client != null)
		{
			var channel = _client.GetChannel(942780457232257044) as SocketTextChannel;

			await _userService.RemoveUserStatsChannel(user, _client);

			await channel.SendMessageAsync($"ББ даун {user.Mention}");
		}
	}
}