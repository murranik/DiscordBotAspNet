using AutoMapper;
using Discord.WebSocket;
using Models;

namespace DTOModels
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<AdministrationGuildDTO, SocketGuild>();
			CreateMap<SocketGuild, AdministrationGuildDTO>();
			CreateMap<AdministratorDto, Administrator>();
			CreateMap<Administrator, AdministratorDto>();
		}
	}
}
