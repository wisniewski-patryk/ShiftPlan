using Blazored.SessionStorage;
using ShiftPlan.Blazor.Commons.Exceptions;
using System.Net.Http.Json;

namespace ShiftPlan.Blazor.Commons.Clients;

public interface IUserIdentityClient
{
	Task RegisterNewUser(UserRegistrationRequest userRegistrationRequest);

	Task LoginUser(UserLoginRequest userData);

	Task LogoutUser();
}

public class UserIdentityClient(HttpClient httpClient, ISessionStorageService sessionStorage) : IUserIdentityClient
{
	private const string loginTokenName = "accessToken";

	public async Task LoginUser(UserLoginRequest userData)
	{
		var response = await httpClient.PostAsync("login", JsonContent.Create(userData));
		if (response.IsSuccessStatusCode)
		{
			var e = await response.Content.ReadFromJsonAsync<UserLoginRespond>();
			await sessionStorage.SetItemAsStringAsync(loginTokenName, e?.AccessToken);
			return;
		}
		throw new HttpCommunicationException(response.ReasonPhrase ?? "Communication error", response.StatusCode);
	}

	public async Task LogoutUser() => await sessionStorage.RemoveItemAsync(loginTokenName);

	public async Task RegisterNewUser(UserRegistrationRequest userData)
	{
		var response = await httpClient.PostAsync("register", JsonContent.Create(userData));
		if (response.IsSuccessStatusCode)
			return;
		throw new HttpCommunicationException($"{response.StatusCode} - Invalid Register request. Password need to contain non-alphanumeric char, numer, at least one big lether."); // TODO: handle many posibility to get bad request - not valid/insecure passwords etc. 
	}
}

public record UserRegistrationRequest(string Email, string Password);

public record UserLoginRequest(string Email, string Password);

public record UserLoginRespond(string TokenType, string AccessToken, string RefreshToken, int ExpiresIn);
