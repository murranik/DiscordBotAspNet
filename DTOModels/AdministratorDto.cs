namespace DTOModels;

public class AdministratorDto
{
	public string Nickname { get; set; }
	public string Email { get; set; }
	public string Password { get; set; }
	public ulong GuildId { get; set; }
	public bool LogedIn = false;
}