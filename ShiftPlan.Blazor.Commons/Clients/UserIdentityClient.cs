using Blazored.SessionStorage;
using System.Net.Http.Json;

namespace ShiftPlan.Blazor.Commons.Clients;

public interface IUserIdentityClient
{
	Task<UserRegistrationResult> RegisterNewUser(UserRegistrationRequest userRegistrationRequest);

	Task<UserLoginResult> LoginUser(UserLoginRequest userData);

	Task LogoutUser();
}

public class UserIdentityClient(HttpClient httpClient, ISessionStorageService sessionStorage) : IUserIdentityClient
{
	private const string loginTokenName = "accessToken";

	public async Task<UserLoginResult> LoginUser(UserLoginRequest userData)
	{
		var result = await httpClient.PostAsync("login", JsonContent.Create(userData));

		if (result is null)
			return new(false);

		var e = await result.Content.ReadFromJsonAsync<UserLoginRespond>();

		await sessionStorage.SetItemAsStringAsync(loginTokenName, e?.AccessToken);
		return new(!string.IsNullOrEmpty(e?.AccessToken));
	}

	public async Task LogoutUser()
	{
		await sessionStorage.RemoveItemAsync(loginTokenName);
	}

	public async Task<UserRegistrationResult> RegisterNewUser(UserRegistrationRequest userData)
	{
		var result = await httpClient.PostAsync("register", JsonContent.Create(userData));
		return new(result.IsSuccessStatusCode);
	}

}

public record UserRegistrationRequest(string Email, string Password);

public record UserRegistrationResult(bool IsSuccess);

public record UserLoginRequest(string Email, string Password);

public record UserLoginResult(bool IsSuccess);

public record UserLoginRespond(string TokenType, string AccessToken, string RefreshToken, int ExpiresIn);
