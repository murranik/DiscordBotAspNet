﻿using Google.Cloud.Firestore;

namespace Models.Firestore
{
	[FirestoreData]
	public class CommandError
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
		public string Message { get; set; }

		[FirestoreProperty]
		public string CommandName { get; set; }
	}
}
