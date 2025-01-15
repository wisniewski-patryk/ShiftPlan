using System.Net.Http.Json;

namespace ShiftPlan.Blazor.WebAssembly.Clients;

public interface IRolesClient
{
	public Task<IEnumerable<IdentityRole>> GetIdentityRoles();

	public Task CreateNewRole(string roleName);

	public Task DeleteRole(string roleName);
}

public class RolesClient(HttpClient client) : IRolesClient
{
	public async Task<IEnumerable<IdentityRole>> GetIdentityRoles()
	{
		var roles = await client.GetAsync("/api/identity/roles/all");
		return await roles.Content.ReadFromJsonAsync<IEnumerable<IdentityRole>>() ?? throw new Exception();
	}

	public async Task CreateNewRole(string roleName)
	{
		var response = await client.PostAsJsonAsync("/api/identity/roles/add", roleName);
		if (!response.IsSuccessStatusCode)
			throw new Exception();
	}

	public async Task DeleteRole(string roleName)
	{
		var response = await client.DeleteAsync($"/api/identity/roles/delete/{roleName}");
		if (!response.IsSuccessStatusCode)
			throw new Exception();
	}
}

public record IdentityRole(string Id, string? Name, string? NormalizedName, string? ConcurrencyStamp);
