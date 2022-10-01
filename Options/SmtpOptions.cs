namespace DiscordBotWebApi.Options;

public class SmtpOptions
{
	public const string Title = "SmtpOptions";
	public string Server { get; set; }
	public int Port { get; set; }
	public string Email { get; set; }
	public string Password { get; set; }
}