using Constants;
using Discord;
using Discord.WebSocket;
using Infrastructure.Models;
using Infrastructure.Services;

namespace Infrastructure.Commands.Images
{
    public class SearchAnimeArtCommand : DiscordSlashCommand
    {
        public override string Name => "searchanimeart";

		public override string Result { get; set; }

		public override async Task ExecuteAsync(DiscordSocketClient client, object commandObj)
        {
            if (commandObj is SocketSlashCommand command)
            {
                var danbooruService = new DanbooruService();
                var url = await danbooruService.GetArt((string)command.Data.Options.First().Value, (string)command.Data.Options.Last().Value);
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
                Description = "Search Anime Art"
            };

            setupRandomArtCommand.AddOption
                (
                "tags",
                ApplicationCommandOptionType.String,
                "Danbouru tag",
                isRequired: true).AddOption
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
