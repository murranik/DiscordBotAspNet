using DTOModels;
using Models;
using System.Text;
using System.Web;

namespace Infrastructure.Services.Administration
{
	public class AuthService
	{
		private readonly AdministrationService _administrationService;
		private readonly SmtpService _smtpService;
		private readonly TokenService _tokenService;

		public AuthService(AdministrationService administrationService, SmtpService smtpService, TokenService tokenService = null)
		{
			_administrationService = administrationService;
			_smtpService = smtpService;
			_tokenService = tokenService;
		}

		public async Task<string> Register(AdministratorDto administrator)
		{
			if (!_administrationService.IsExistEmail(administrator.Email))
			{
				var newAdministrator = new Administrator()
				{
					Id = Guid.NewGuid().ToString(),
					ConfirmEmail = false,
					Email = administrator.Email,
					GuildId = administrator.GuildId,
					Nickname = administrator.Nickname,
					Password = administrator.Password,
				};

				await _administrationService.UpsertAdministratorAsync(newAdministrator);
				var token = _tokenService.GenerateToken(newAdministrator.Email + newAdministrator.Nickname + newAdministrator.Password);

				_smtpService.SendMail2Step(
					newAdministrator.Email,
					Subject: "Shiki admin email confirm",
					Body: $"<a href=\"https://localhost:7160/api/Auth/ConfirmEmail?AdministratorId={newAdministrator.Id}&Token={token}\">confirm link</a>"
				);

				return string.Empty;
			}
			else
			{
				return "Email is exist";
			}
		}

		public async Task ConfirmEmail(string administratorId, string token) 
		{
			if (!string.IsNullOrEmpty(token))
			{
				var administrator = await _administrationService.FindAdministratorAsyns(administratorId);
				if (administrator != null) 
				{
					var validationToken = _tokenService.GenerateToken(administrator.Email + administrator.Nickname + administrator.Password);

					if (validationToken == token) 
					{
						administrator.ConfirmEmail = true;
						await _administrationService.UpsertAdministratorAsync(administrator);
					}
				}
			}
		}
	}
}
