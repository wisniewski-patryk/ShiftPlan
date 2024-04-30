using FrontendBlazor.Client.Models;
using System.Net.Http.Json;

namespace FrontendBlazor.Client.Clients;

public interface IEmployeesClient
{
	Task<Employee?> Get(int id);
	Task<IEnumerable<Employee>?> GetAll();
	Task<Employee?> InsertOrUpdate(Employee employee);
	Task Remove(Employee employee);
}

public class EmployeesClient(HttpClient httpClient) : IEmployeesClient
{
	public async Task<IEnumerable<Employee>?> GetAll()
	{
		return await httpClient.GetFromJsonAsync<Employee[]>("employees");
	}

	public async Task<Employee?> Get(int id)
	{
		return await httpClient.GetFromJsonAsync<Employee>($"employees/{id}");
	}

	public async Task<Employee?> InsertOrUpdate(Employee employee)
	{
		var response = await httpClient.PostAsJsonAsync("employees/insertOrUpdate", employee);
		response.EnsureSuccessStatusCode();
		return await response.Content.ReadFromJsonAsync<Employee>();
	}

	public async Task Remove(Employee employee)
	{
		var request = new HttpRequestMessage(HttpMethod.Delete, "employees/delete")
        {
            Content = JsonContent.Create(employee)
        };
		var response = await httpClient.SendAsync(request);
		response.EnsureSuccessStatusCode();
	}
}
