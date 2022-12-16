using Infrastructure.Commands;
using Interfaces;

namespace Infrastructure.Helpers
{
	public static class CommandResultParser
	{
		private static Dictionary<string, string> _results = new()
		{ 
			{ "searchanimeart","" },
			{ "randomanimeart","" },
			{ "random","" },
			{ "roll","" },
			{ "sendcalendar","" },
			{ "searchanime","" },
			{ "clear","" },
			{ "commandhistory","" },
			{ "jointochat","" },
			{ "!setup","" },
			{ "testcipher","" },
		};

		public static string Parse(ICommand command) 
		{
			_results.TryGetValue(command.Name, out var res);
			return res;
		}
	}
}
