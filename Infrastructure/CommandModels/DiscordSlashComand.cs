using Discord;
using Discord.WebSocket;
using Interfaces;

namespace Infrastructure.Models
{
    public abstract class DiscordSlashCommand : ICommand
    {
        public abstract string Name { get; }
		public abstract string Result { get; set; }

		public abstract Task ExecuteAsync(DiscordSocketClient client, object commandObj);

        public abstract SlashCommandBuilder GetSlashCommandBuilder();
    }
}
