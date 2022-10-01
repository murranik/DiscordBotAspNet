namespace Interfaces;

public interface IDanbooruService
{
	Task<string> GetRandomArt(bool cencorship = false);
	Task<string> GetArt(string tags, bool cencorship = false);
}