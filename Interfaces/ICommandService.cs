using Discord.WebSocket;

namespace Interfaces
{
	public interface ICommandService
	{
		List<ICommand> Commands { get; }
		ICommand GetComand(SocketSlashCommand msg);
		ICommand GetComand(SocketMessage msg);

	}
}
