using AutoMapper;
using Discord.WebSocket;
using DTOModels;
using Interfaces;
using Interfaces.Administration;
using Models;

namespace Infrastructure.Services.Administration;

public class GetService : IGetService
{
	private readonly IUserService _userService;
	private readonly IAdministrationService _administrationService;
	private readonly DiscordSocketClient _client;
	private readonly IMapper _mapper;

	public GetService(IUserService userService, DiscordSocketClient client, IMapper mapper, IAdministrationService administrationService)
	{
		_userService = userService;
		_client = client;
		_mapper = mapper;
		_administrationService = administrationService;
	}

	public async Task<List<object>> GetAllAsync(Type type)
	{
		var result = new List<object>();
		if (type == typeof(DiscordUser))
		{
			foreach (var user in await _userService.GetAllUsers())
			{
				result.Add(user);
			}
		} 
		else if (type == typeof(DiscordRole))
		{
			foreach (var role in await _userService.GetAllRoles())
			{
				result.Add(role);
			}
		}
		else if (type == typeof(Administrator))
		{
			foreach (var administrator in _administrationService.GetAdministrators())
			{
				result.Add(_mapper.Map<AdministratorDto>(administrator));
			}
		}
		else if (type == typeof(SocketGuild))
		{
			foreach (var guild in _client.Guilds)
			{
				result.Add(_mapper.Map<AdministrationGuildDto>(guild));
			}
		}

		return result;
	}
}