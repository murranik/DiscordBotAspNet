using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Interfaces;
using Models.Firestore;

namespace Infrastructure.Services
{
	public class AuditService : IAuditService
	{
		private readonly FirestoreDb _firestoreDb;

		public AuditService() 
		{
			var jsonString = File.ReadAllText("D:\\Projects\\shikiwebui-e7009-firebase-adminsdk-58kja-54de9d402b.json");
			var builder = new FirestoreClientBuilder { JsonCredentials = jsonString };
			_firestoreDb = FirestoreDb.Create("shikiwebui-e7009", builder.Build());
		}

		public async Task AddCommandCallInfoAsync(CommandCallInfo command, ulong guildId)
		{
			await _firestoreDb
				.Collection("audit")
				.Document($"{guildId}")
				.Collection("CommandsCallInfo")
				.AddAsync(command);
		}

		public async Task<List<CommandCallInfo>> GetCommandsHistory(int? rowsCount, ulong guildId)
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
}
