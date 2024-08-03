using ShiftPlan.Blazor.Client.Clients;

namespace ShiftPlan.Blazor.Client.Services;


public interface IUserIdentityService
{
	Task<bool> LoginUser(string email, string password);

	Task<bool> RegisterNewUser(string email, string password);

	Task LogoutUser();
}

public class UserIdentityService(IUserIdentityClient client) : IUserIdentityService
{
	public async Task<bool> LoginUser(string email, string password)
	{
		var loginResult = await client.LoginUser(new(email, password)) ?? throw new Exception("Login fail");
		return loginResult.IsSuccess;
	}

	public async Task LogoutUser()
	{
		await client.LogoutUser();
	}

	public async Task<bool> RegisterNewUser(string email, string password)
	{
		var result = await client.RegisterNewUser(new(email, password));
		return result.IsSuccess;
	}
}
