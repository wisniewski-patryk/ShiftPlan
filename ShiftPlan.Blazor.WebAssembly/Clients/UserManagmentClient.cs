using System.Net.Http.Json;

namespace ShiftPlan.Blazor.WebAssembly.Clients;

public class UserManagmentClient(HttpClient client)
{
	public async Task<IEnumerable<UserWithRoles>> GetAllUsers()
	{
		var response = await client.GetAsync("/api/identity/users/withroles");
		return await response.Content.ReadFromJsonAsync<IEnumerable<UserWithRoles>>() ?? throw new Exception();
	}

}

public record UserDto(string Id, string UserName, string Email);
public record UserWithRoles(UserDto User, IEnumerable<string> Roles);
