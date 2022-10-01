using Models.Firestore;

namespace Interfaces;

public interface IAuditService
{
	Task AddErrorAsync(CommandError error, ulong guildId);

	Task AddCommandAsync(CommandCallInfo command, ulong guildId);

	Task<List<CommandError>> GetErrorsAsync(int? rowsCount, ulong guildId);

	Task<List<CommandCallInfo>> GetCommandHistory(int? rowsCount, ulong guildId);
}