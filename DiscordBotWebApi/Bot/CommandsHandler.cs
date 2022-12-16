using Discord.WebSocket;
using Interfaces;
using Models.Firestore;

namespace DiscordBotWebApi.Bot
{
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
                        await _auditService.AddCommandCallInfoAsync(
                            BuildCommandCallInfo(
                                command: command,
                                commandResult: command.Result,
                                userCommand: commandData
                            ),
                            (commandData.Channel as SocketGuildChannel).Guild.Id
                        );
                    }
                    catch (Exception e)
                    {
                        await _auditService.AddCommandCallInfoAsync(
                            BuildCommandCallInfo(
                                eMesaage: e.Message,
                                command: command,
                                commandResult: command.Result,
                                userCommand: commandData
                            ),
                            (commandData.Channel as SocketGuildChannel).Guild.Id);
                        await commandData.RespondAsync("An unexpected error occured");
                    }
                }
                else
                {
                    await _auditService.AddCommandCallInfoAsync(
                        BuildCommandCallInfo(
                            eMesaage: $"Error while user {commandData.User.Username} try to call command named **{commandData.CommandName}** - command not found",
                            command: command,
                            commandResult: command.Result,
                            userCommand: commandData
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
                        await _auditService.AddCommandCallInfoAsync(
                            BuildCommandCallInfo(
                                command: command,
                                commandResult: command.Result,
                                userCommand: msg
                            ),
                            (msg.Channel as SocketGuildChannel).Guild.Id
                        );
                    }
                    catch (Exception e)
                    {

                        await _auditService.AddCommandCallInfoAsync(
                            BuildCommandCallInfo(
                                eMesaage: e.Message,
                                command: command,
                                commandResult: command.Result,
                                userCommand: msg
                            ),
                            (msg.Channel as SocketGuildChannel).Guild.Id);
                        await msg.Channel.SendMessageAsync("An unexpected error occured");
                    }

                }
                else if (msg.Content.StartsWith('!'))
                {
                    await _auditService.AddCommandCallInfoAsync(
                        BuildCommandCallInfo(
                            eMesaage: $"Error while user {msg.Author.Username} try to call command named **{msg.Content}** - command not found",
                            command: command,
                            commandResult: command.Result,
                            userCommand: msg
                        ),
                        (msg.Channel as SocketGuildChannel).Guild.Id);
                    await msg.Channel.SendMessageAsync("Command not found");
                }
            }
        }

        private CommandCallInfo BuildCommandCallInfo(
            object userCommand = null,
            ICommand command = null,
            string eMesaage = null,
            string commandResult = null
            )
        {
            dynamic tempCommand = userCommand;
            ulong userId = 0;
            string userName = "";
            List<string> commandParams = new();

            if (userCommand is SocketMessage cmd) 
            {
                tempCommand = userCommand as SocketMessage;
                userId = cmd.Author.Id;
                userName = cmd.Author.Username;
                commandParams = cmd.Content.Replace(command.Name, "").Split().ToList();
            }

            if (userCommand is SocketSlashCommand socketCmd)
            {
                tempCommand = userCommand as SocketSlashCommand;
                userId = socketCmd.User.Id;
                userName = socketCmd.User.Username;
                commandParams = socketCmd.Data.Options.Select(x => x.Name).ToList();
            }

            return new CommandCallInfo()
            {
                Id = Guid.NewGuid().ToString(),
                Date = DateTime.Now.ToString("dd/MM/yyyy:H:m"),
                GuildName = (tempCommand.Channel as SocketGuildChannel).Guild.Name,
                UserId = userId,
                CommandName = command.Name,
                CommandParams = commandParams,
                CommandResult = commandResult,
                ErrorMessage = eMesaage,
            };
        }
    }
}
