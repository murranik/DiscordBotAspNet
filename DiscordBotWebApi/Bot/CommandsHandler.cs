using Discord.WebSocket;
using Interfaces;
using Models.Firestore;

namespace DiscordBotWebApi.Bot;

public class CommandsHandler
{
    private readonly DiscordSocketClient _client;
    private readonly ICommandService _commandServices;
    private readonly IAuditService _auditService;
    private readonly IUserService _userService;

    public CommandsHandler(
        DiscordSocketClient client,
        ICommandService commandServices,
        IUserService userService,
        IAuditService auditService)
    {
        _client = client;
        _commandServices = commandServices;
        _userService = userService;
        _auditService = auditService;
    }

    public async Task Handler(SocketSlashCommand commandData)
    {
        if (!commandData.User.IsBot)
        {
            await _userService.AddPointsFotUser(commandData.User.Id.ToString(), 1);
            var command = _commandServices.GetComand(commandData);

            if (command != null)
            {
                try
                {
                    await command.ExecuteAsync(_client, commandData);
                    await _auditService.AddCommandAsync(
                        BuildCommandCallInfo(
                            commandData,
                            command
                        ),
                        (commandData.Channel as SocketGuildChannel).Guild.Id
                    );
                }
                catch (Exception e)
                {
                    await _auditService.AddErrorAsync(
                        BuildCommandError(
                            commandData,
                            command,
                            e.Message
                        ),
                        (commandData.Channel as SocketGuildChannel).Guild.Id);
                    await commandData.RespondAsync("An unexpected error occured");
                }
            }
            else
            {
                await _auditService.AddErrorAsync(
                    BuildCommandError(
                        commandData,
                        command,
                        $"Error while user {commandData.User.Username} try to call command named **{commandData.CommandName}** - command not found"
                    ),
                    (commandData.Channel as SocketGuildChannel).Guild.Id);
                await commandData.RespondAsync("Command not found");
            }
        }
    }

    public async Task Handler(SocketMessage msg)
    {
        await _userService.AddPointsFotUser(msg.Author.Id.ToString(), 1);

        var command = _commandServices.GetComand(msg);

        if (!msg.Author.IsBot)
        {
            if (command != null)
            {
                try
                {
                    await command.ExecuteAsync(_client, msg);
                    await _auditService.AddCommandAsync(
                        BuildCommandCallInfo(
                            msg,
                            command
                        ),
                        (msg.Channel as SocketGuildChannel).Guild.Id
                    );
                }
                catch (Exception e)
                {

                    await _auditService.AddErrorAsync(
                        BuildCommandError(
                            msg,
                            command,
                            e.Message
                        ),
                        (msg.Channel as SocketGuildChannel).Guild.Id);
                    await msg.Channel.SendMessageAsync("An unexpected error occured");
                }

            }
            else if (msg.Content.StartsWith('!'))
            {
                await _auditService.AddErrorAsync(
                    BuildCommandError(
                        msg,
                        command,
                        $"Error while user {msg.Author.Username} try to call command named **{msg.Content}** - command not found"
                    ),
                    (msg.Channel as SocketGuildChannel).Guild.Id);
                await msg.Channel.SendMessageAsync("Command not found");
            }
        }
    }

    private CommandError BuildCommandError(object userCommand, ICommand command, string errorText)
    {
        return new CommandError()
        {
            Date = DateTime.Now.ToString("dd/MM/yyyy:H:m"),
            GuildName = userCommand is SocketMessage
                ? ((userCommand as SocketMessage).Channel as SocketGuildChannel).Guild.Name
                : ((userCommand as SocketSlashCommand).Channel as SocketGuildChannel).Guild.Name,
            Id = Guid.NewGuid().ToString(),
            UserId = userCommand is SocketMessage 
                ? (userCommand as SocketMessage).Author.Id 
                : (userCommand as SocketSlashCommand).User.Id,
            Message = errorText,
            CommandName = command.Name
        };
    }

    private CommandCallInfo BuildCommandCallInfo(object userCommand, ICommand command)
    {
        dynamic tempCommand = userCommand;
        ulong userId = 0;
        string userName = "";

        if (userCommand is SocketMessage cmd) 
        {
            tempCommand = userCommand as SocketMessage;
            userId = cmd.Author.Id;
            userName = cmd.Author.Username;
        }

        if (userCommand is SocketSlashCommand socketCmd)
        {
            tempCommand = userCommand as SocketSlashCommand;
            userId = socketCmd.User.Id;
            userName = socketCmd.User.Username;
        }

        return new CommandCallInfo()
        {
            Id = Guid.NewGuid().ToString(),
            Date = DateTime.Now.ToString("dd/MM/yyyy:H:m"),
            GuildName = (tempCommand.Channel as SocketGuildChannel).Guild.Name,
            UserId = userId,
            Message = $"User {userName} run command named **{command.Name}**",
        };
    }
}