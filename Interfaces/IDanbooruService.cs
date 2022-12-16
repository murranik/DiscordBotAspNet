namespace Interfaces
{
	public interface IDanbooruService
	{
		Task<string> GetRandomArt(string provider);
		Task<string> GetArt(string tags, string provider);
	}
}
