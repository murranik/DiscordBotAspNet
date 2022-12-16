using Ardalis.Specification;
using Models;

namespace Infrastructure.Specifications;

public class CheckIsRoleExistSpec : Specification<DiscordRole>, ISingleResultSpecification<DiscordRole>
{
    public CheckIsRoleExistSpec(ulong discordId)
    {
        Query.Where(x => x.DiscordId == discordId);
    }
}