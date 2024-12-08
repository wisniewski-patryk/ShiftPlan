using System.Net.Http.Json;

namespace ShiftPlan.Blazor.WebAssembly.Clients;

public class UserManagmentClient(HttpClient client)
{
	public async Task<IEnumerable<UserWithRoles>> GetAllUsers()
	{
		var response = await client.GetAsync("/api/identity/users/withroles");
		return await response.Content.ReadFromJsonAsync<IEnumerable<UserWithRoles>>() ?? throw new Exception();
	}

	public async Task AssignRoleToUser(string userEmail, string roleName)
	{
		var response = await client.PostAsJsonAsync("/api/identity/users/assigment/add", new AssignRoleRequest(userEmail, roleName));
		if (!response.IsSuccessStatusCode)
			throw new Exception();
	}

	public async Task RemoveRoleFromUser(string userEmail, string roleName)
	{
		var response = await client.PostAsJsonAsync("/api/identity/users/assigment/remove", new RemoveRoleRequest(userEmail, roleName));
		if (!response.IsSuccessStatusCode)
			throw new Exception();
	}
}

public record UserDto(string Id, string UserName, string Email);
public record UserWithRoles(UserDto User, IEnumerable<string> Roles);

public record AssignRoleRequest(string UserEmail, string RoleName);

public record RemoveRoleRequest(string UserEmail, string RoleName);
