using ShiftPlan.Blazor.Commons.Clients;

namespace ShiftPlan.Blazor.Commons.Services;

public interface IUserIdentityService
{
	Task LoginUser(string email, string password);

	Task RegisterNewUser(string email, string password);

	Task LogoutUser();
}

public class UserIdentityService(IUserIdentityClient client) : IUserIdentityService
{
	public async Task LoginUser(string email, string password) => await client.LoginUser(new(email, password));

	public async Task LogoutUser() => await client.LogoutUser();

	public async Task RegisterNewUser(string email, string password) => await client.RegisterNewUser(new(email, password));
}
