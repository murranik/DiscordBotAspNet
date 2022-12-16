using Google.Cloud.Firestore;

namespace Models.Firestore
{
	[FirestoreData]
	public class CommandCallInfo
	{
		[FirestoreProperty]
		public string Id { get; set; }

		[FirestoreProperty]
		public string Date { get; set; }

		[FirestoreProperty]
		public string GuildName { get; set; }

		[FirestoreProperty]
		public ulong UserId { get; set; }

		[FirestoreProperty]
		public string CommandName { get; set; }

		[FirestoreProperty]
		public List<string> CommandParams { get; set; }

		[FirestoreProperty]
		public string CommandResult { get; set; }

		[FirestoreProperty]
		public string? ErrorMessage { get; set; }
	}
}
