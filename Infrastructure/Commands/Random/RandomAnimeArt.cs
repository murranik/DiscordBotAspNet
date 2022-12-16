using Constants;
using Discord;
using Discord.WebSocket;
using Infrastructure.Models;
using Infrastructure.Services;

namespace Infrastructure.Commands.RandomCommands
{
    public class RandomAnimeArt : DiscordSlashCommand
    {
        public override string Name => "randomanimeart";
        public override string Result { get; set; }

        public override async Task ExecuteAsync(DiscordSocketClient client, object commandObj)
        {
            if (commandObj is SocketSlashCommand command)
            {
                var danbooruService = new DanbooruService();
                var url = await danbooruService.GetRandomArt(command.Data.Options.First().Value as string);
                Result = url;
                await command.RespondAsync(url ?? "Not found");
            }
            else
            {
                return;
            }
        }

        public override SlashCommandBuilder GetSlashCommandBuilder()
        {
            var setupRandomArtCommand = new SlashCommandBuilder
            {
                Name = Name,
                Description = "Send random art"
            };

            setupRandomArtCommand.AddOption
                (
                "provider",
                ApplicationCommandOptionType.String,
                "nani?",
                isRequired: true,
                choices: ConstantsContainer
                    .AnimeArtsProviders
                        .Select(x => new ApplicationCommandOptionChoiceProperties
                        {
                            Name = x.Key,
                            Value = x.Value.ToString(),
                        })
                        .ToArray()
                );

            return setupRandomArtCommand;
        }
    }
}
