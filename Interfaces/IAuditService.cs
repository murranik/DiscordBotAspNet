using Models.Firestore;

namespace Interfaces
{
	public interface IAuditService
	{
		Task AddCommandCallInfoAsync(CommandCallInfo command, ulong guildId);

		Task<List<CommandCallInfo>> GetCommandsHistory(int? rowsCount, ulong guildId);
	}
}
