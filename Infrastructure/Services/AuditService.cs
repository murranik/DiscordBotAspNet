using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Interfaces;
using Models.Firestore;

namespace Infrastructure.Services;

public class AuditService : IAuditService
{
	private readonly FirestoreDb _firestoreDb;

	//TODO 
	public AuditService() 
	{
		var jsonString = File.ReadAllText("shikiwebui-e7009-firebase-adminsdk-58kja-54de9d402b.json");
		var builder = new FirestoreClientBuilder { JsonCredentials = jsonString };
		_firestoreDb = FirestoreDb.Create("shikiwebui-e7009", builder.Build());
	}

	public async Task AddErrorAsync(CommandError error, ulong guildId)
	{
		await _firestoreDb
			.Collection("audit")
			.Document($"{guildId}")
			.Collection("Errors")
			.AddAsync(error);
	}

	public async Task AddCommandAsync(CommandCallInfo command, ulong guildId)
	{
		await _firestoreDb
			.Collection("audit")
			.Document($"{guildId}")
			.Collection("CommandsCallInfo")
			.AddAsync(command);
	}

	public async Task<List<CommandError>> GetErrorsAsync(int? rowsCount, ulong guildId) 
	{
		var errorsSnapshot = await _firestoreDb
			.Collection("audit")
			.Document($"{guildId}")
			.Collection("Errors")
			.GetSnapshotAsync();

		List<CommandError> commandErrors = new();

		errorsSnapshot
			.Documents
			.ToList()
			.ForEach(x => commandErrors.Add(x.ConvertTo<CommandError>()));

		return commandErrors;
	}

	public async Task<List<CommandCallInfo>> GetCommandHistory(int? rowsCount, ulong guildId)
	{
		var commandsCallInfoSnapshot = await _firestoreDb
			.Collection("audit")
			.Document($"{guildId}")
			.Collection("CommandsCallInfo")
			.GetSnapshotAsync();

		List<CommandCallInfo> commandCallInfos = new();

		commandsCallInfoSnapshot
			.Documents
			.ToList()
			.ForEach(x => commandCallInfos.Add(x.ConvertTo<CommandCallInfo>()));

		return commandCallInfos;
	}
}