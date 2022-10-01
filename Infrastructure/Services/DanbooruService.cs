using Interfaces;
using System.Text.Json;

namespace Infrastructure.Services;

public class DanbooruService : IDanbooruService
{
    private readonly HttpClient _httpClient = new();   

    public async Task<string> GetRandomArt(bool cencorship = false) 
    {
        string domain;

        if (cencorship)
            domain = "https://danbooru.donmai.us/";
        else
            domain = "https://safebooru.donmai.us/";

        Console.WriteLine("cens = " + !cencorship + " domain " + domain);

        var res = await _httpClient.GetAsync(domain + "posts/random.json");
        var data = JsonSerializer.Deserialize<Dictionary<string, object>>(await res.Content.ReadAsStringAsync());
        var fileUrlDictionary = data.FirstOrDefault(x => x.Key == "file_url");
        var notExistUrl = fileUrlDictionary.Value == null;

        Console.WriteLine("NotExistUrl = " + notExistUrl);

        while (notExistUrl) 
        {
            return null;
        }

        return data["file_url"].ToString();
    }

    public async Task<string> GetArt(string tags, bool cencorship = false)
    {
        string domain;
        if (cencorship)
            domain = "https://danbooru.donmai.us/";
        else
            domain = "https://safebooru.donmai.us/";

        Console.WriteLine("cens = " + !cencorship + " domain " + domain);
        var res = await _httpClient.GetAsync(domain + $"posts/random.json?tags={tags}");
        var data = JsonSerializer.Deserialize<Dictionary<string, object>>(await res.Content.ReadAsStringAsync());
        var fileUrlDictionary = data.FirstOrDefault(x => x.Key == "file_url");
        var ifNotExistUrl = fileUrlDictionary.Value == null;
        Console.WriteLine("ifNotExistUrl = " + ifNotExistUrl);
        while (ifNotExistUrl)
        {
            return null;
        }

        return data["file_url"].ToString();
    }
}