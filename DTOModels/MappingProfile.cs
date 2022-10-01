using AutoMapper;
using Discord.WebSocket;
using Models;

namespace DTOModels;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<AdministrationGuildDto, SocketGuild>();
		CreateMap<SocketGuild, AdministrationGuildDto>();
		CreateMap<AdministratorDto, Administrator>();
		CreateMap<Administrator, AdministratorDto>();
	}
}