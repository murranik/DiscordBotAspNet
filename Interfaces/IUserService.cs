using Discord.WebSocket;
using Models;

namespace Interfaces
{
	public interface IUserService
	{
		Task GeneratePersonalStatisticChats(DiscordSocketClient discordSocketClient);
		Task GeneratePersonalStatisticChat(SocketGuildUser user, DiscordSocketClient discordSocketClient);
		Task RemoveUsersStatsChannel(DiscordSocketClient discordSocketClient);
		Task RemoveUserStatsChannel(SocketUser user, DiscordSocketClient discordSocketClient);
		Task FetchAllUsersFromDiscord(DiscordSocketClient discordSocketClient);
		Task FetchAllRolesFromDiscord(DiscordSocketClient discordSocketClient);
		Task AddUser(DiscordUser discordUser);
		Task AddRole(DiscordRole role);
		Task UpdateRole(DiscordRole role);
		Task RemoveRole(ulong roleId);
		Task<int> AddPointsFotUser(string userId, int pointsCount);
		Task<List<DiscordUser>> GetAllUsers();
		Task<List<DiscordRole>> GetAllRoles();
	}
}
