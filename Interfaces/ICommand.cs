using Discord;
using Discord.WebSocket;

namespace Interfaces
{
	public interface ICommand
    {
        public abstract string Name { get; }
        public abstract string Result { get; set; }
        
        public bool Contains(SocketMessage msg)
        {
            if (msg.Author.IsBot)
                return false;
            if (msg.Content.Contains(Name))
                return true;
            return false;
        }

        public SlashCommandBuilder GetSlashCommandBuilder();

        public Task ExecuteAsync(DiscordSocketClient client, object commandObj);
    }
}
