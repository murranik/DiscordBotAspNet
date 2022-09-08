using DTOModels;
using Models;

namespace Interfaces.Administration
{
	public interface IAdministrationService
	{
		Task UpsertAdministratorAsync(Administrator administrator);
		bool Login(AdministratorDTO administrator);
		List<Administrator> GetAdministrators();
		Task<Administrator> FindAdministratorAsyns(string id);
		bool IsExistEmail(string administratorEmail);
	}
}
