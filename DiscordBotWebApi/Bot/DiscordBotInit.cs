using Discord;
using Discord.WebSocket;
using DiscordBotWebApi.Bot.Handlers;
using Infrastructure.Services;
using Infrastructure.Services.Administration;
using Interfaces;
using Interfaces.Administration;

namespace DiscordBotWebApi.Bot;

public static class DiscordBotInit
{
    public static IServiceCollection AddClient(this IServiceCollection services, IConfiguration configuration)
    {   
        DiscordSocketClient client = new(new DiscordSocketConfig()
        {
            AlwaysDownloadUsers = true,
            GatewayIntents = GatewayIntents.All,

        });

        var provider = services.BuildServiceProvider();
        var commandService = provider.GetService(typeof(ICommandService)) as ICommandService;
        var userService = provider.GetService(typeof(IUserService)) as IUserService;
        var administrationService = provider.GetService(typeof(IAdministrationService)) as IAdministrationService;
        var tokenService = provider.GetService(typeof(ITokenService)) as ITokenService;
        var auditService = provider.GetService(typeof(IAuditService)) as IAuditService;

        client.Log += Log;
        client.MessageReceived += new CommandsHandler(client, commandService, userService, auditService).Handler;
        client.SlashCommandExecuted += new CommandsHandler(client, commandService, userService, auditService).Handler;
        client.UserJoined += new OnMemberJoinHandler(client, userService).MessageSender;
        client.UserLeft += new OnMemberLeftHandler(client, userService).Handler;
        client.RoleCreated += new OnRoleCreatedHandler(userService).Handler;
        client.RoleDeleted += new OnRoleDeleteHandler(userService).Handler;
        client.RoleUpdated += new OnRoleUpdateHandler(userService).Handler;
        client.JoinedGuild += new OnJoinedGuildHandler(administrationService, tokenService).Handler;
        //client.LeftGuild += new OnLeftGuildHandler(administrationService).Handler;
        client.Ready += OnReadyAsync;

        client.SetGameAsync("Refactoring facebook API");
        client.LoginAsync(TokenType.Bot, configuration.GetValue<string>("BotToken"));
        client.StartAsync();
        services.AddSingleton(client);

        return services;
    }

    private static Task OnReadyAsync()
    {
        return Task.CompletedTask;
    }
    private static Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }
}