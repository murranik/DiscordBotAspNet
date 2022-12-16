using Discord;
using Discord.WebSocket;
using Interfaces;

namespace Infrastructure.Models
{
    public abstract class DiscordAPICommand : ICommand
    {
        public abstract string Name { get; }
		public abstract string Result { get; set; }

		public abstract Task ExecuteAsync(DiscordSocketClient client, object data);

        public abstract SlashCommandBuilder GetSlashCommandBuilder();
    }
}
