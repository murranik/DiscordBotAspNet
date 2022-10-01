using Discord;
using Discord.WebSocket;
using Interfaces;

namespace Infrastructure.Models;

public abstract class DiscordApiCommand : ICommand
{
    public abstract String Name { get; }

    public abstract Task ExecuteAsync(DiscordSocketClient client, object data);

    public abstract SlashCommandBuilder GetSlashCommandBuilder();
}