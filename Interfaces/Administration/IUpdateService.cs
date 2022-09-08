namespace Interfaces.Administration
{
	public interface IUpdateService
	{
		Task ChangeUserPrestigeLevelAsync(string userId, int newLevel);
		Task UpdateModelAsync<T>(T data, Type type) where T : class;


	}
}
