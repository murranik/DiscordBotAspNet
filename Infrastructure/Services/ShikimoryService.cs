using DiscordBotWebApi.Options.Shikimory;
using Enums;
using Interfaces;
using Microsoft.Extensions.Logging;
using ShikimoriSharp;
using ShikimoriSharp.Bases;
using ShikimoriSharp.Classes;
using ShikimoriSharp.Settings;


namespace Infrastructure.Services;

public class ShikimoryService : IShikimoryService
{
    private readonly ShikimoryClientOptions _options;
    private readonly ShikimoriClient _client;

    public ShikimoryService(ShikimoryClientOptions options) 
    {
        _options = options;
        _client = new ShikimoriClient(
            new Logger<ShikimoryService>(new LoggerFactory()),
            new ClientSettings(_options.ClientName, _options.ClientId, _options.ClientSecret)
        );
    }

    public async Task<Calendar[]> FetchShikimoryCalendarData(ShikimoryCalendarFetchParametr parametr, int? day = null)
    {
        switch (parametr)
        {
            case ShikimoryCalendarFetchParametr.Today: 
            {                    
                var calendar = await _client.Calendars.GetCalendar();

                return calendar
                    .Where(x => x.NextEpisodeAt.Day == DateTime.Now.Date.Day && x.Anime.EpisodesAired != 0)
                    .ToArray();
            };
            case ShikimoryCalendarFetchParametr.DayOfWeek:
            {
                var calendar = await _client.Calendars.GetCalendar();

                return calendar
                    .Where(x => (int)x.NextEpisodeAt.DayOfWeek == day && x.Anime.EpisodesAired != 0)
                    .ToArray();
            };
            case ShikimoryCalendarFetchParametr.DayOfMonth:
            {
                var calendar = await _client.Calendars.GetCalendar();

                return calendar
                    .Where(x => x.NextEpisodeAt.Day == day && x.Anime.EpisodesAired != 0)
                    .ToArray();
            };
            default: return null;            
        }
    }

    public async Task<Anime[]> SearchAnimeByQueryString(string query) 
    {
        var animeRequestSettings = new AnimeRequestSettings
        {
            search = query
        };

        var result = await _client.Animes.GetAnime(
            animeRequestSettings
        );

        return result;
    }
}