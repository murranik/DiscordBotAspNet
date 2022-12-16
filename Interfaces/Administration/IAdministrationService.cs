using DTOModels;
using Models;

namespace Interfaces.Administration;

public interface IAdministrationService
{
	Task UpsertAdministratorAsync(Administrator administrator);
	bool Login(AdministratorDto administrator);
	List<Administrator> GetAdministrators();
	Task<Administrator> FindAdministratorAsync(string id);
	bool IsExistEmail(string administratorEmail);
}