using Enums;
using ShikimoriSharp.Classes;
namespace Interfaces
{
	public interface IShikimoryService
	{
		Task<Calendar[]> FetchShikimoryCalendarData(ShikimoryCalendarFetchParametr parametr, int? day = null);
		Task<Anime[]> SearchAnimeByQueryString(string query);

	}
}
