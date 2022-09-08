namespace Interfaces.Administration
{
	public interface IGetService
	{
		Task<List<object>> GetAllAsync(Type type);

	}
}
