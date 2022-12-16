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
			CreateMap<SocketGuild, AdministrationGuildDTO>()
				.ForMember(x => x.Id, y => y.MapFrom(z => z.Id.ToString()));
			CreateMap<AdministratorDto, Administrator>();
			CreateMap<Administrator, AdministratorDto>();
		}
	}
}
