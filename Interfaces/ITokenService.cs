namespace Interfaces
{
	public interface ITokenService
	{
		string Encrypt(string data);
		string Decrypt(string cipheredData);
		string GenerateToken(string data);
	}
}
