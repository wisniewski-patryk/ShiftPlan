using Blazored.SessionStorage;
using ShiftPlan.Blazor.Commons.Exceptions;
using System.Net;
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
    
		throw response.StatusCode switch
		{
			HttpStatusCode.BadRequest => new HttpCommunicationException("Invalid login request. Password need to contain non-alphanumeric char, numer, at least one big lether.", response.StatusCode),
			HttpStatusCode.Unauthorized => new HttpCommunicationException("Authentication failed. Please check your credentials and try again.", response.StatusCode),
			_ => new HttpCommunicationException(response.ReasonPhrase ?? "Communication error", response.StatusCode),
		};
	}

	public async Task LogoutUser() => await sessionStorage.RemoveItemAsync(loginTokenName);

	public async Task RegisterNewUser(UserRegistrationRequest userData)
	{
		var response = await httpClient.PostAsync("register", JsonContent.Create(userData));

		var responseAsObject = await response.Content.ReadFromJsonAsync<RegistrationResponse>();
		if (response.IsSuccessStatusCode)
			return;

		throw (responseAsObject?.Errors) switch
		{
			{ InvalidEmail: not null } => new HttpCommunicationException("The provided email address is invalid. Please enter a valid email address."),
			{ DuplicateUserName: not null } => new HttpCommunicationException("The email address is already in use. Please use a different email address."),
			{ PasswordRequiresNonAlphanumeric: not null } or
			{ PasswordRequiresDigit: not null } or
			{ PasswordRequiresUpper: not null } => new HttpCommunicationException("Password should contain at least 8 characters, including at least one uppercase letter, one digit, and one non alphanumeric character."),
			_ => new HttpCommunicationException($"{response.StatusCode} - Invalid Registration request. Please contact with support."),
		};
	}
}

public record UserRegistrationRequest(string Email, string Password);

public record UserLoginRequest(string Email, string Password);

public record UserLoginRespond(string TokenType, string AccessToken, string RefreshToken, int ExpiresIn);

public record RegistrationResponse(string Type, string Title, int Status, Errors Errors);
public record Errors(string[]? PasswordRequiresNonAlphanumeric, string[]? PasswordRequiresDigit, string[]? PasswordRequiresUpper, string[]? DuplicateUserName, string[]? InvalidEmail);