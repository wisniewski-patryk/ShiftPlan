using Blazored.SessionStorage;
using ShiftPlan.Blazor.Client.Models;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ShiftPlan.Blazor.Client.Clients;

public interface IEmployeesClient
{
	Task<Employee?> Get(int id);
	Task<IEnumerable<Employee>?> GetAll();
	Task<Employee?> InsertOrUpdate(Employee employee);
	Task Remove(Employee employee);
}

public class EmployeesClient(HttpClient httpClient, ISessionStorageService sessionStorage) : IEmployeesClient
{
	public async Task<IEnumerable<Employee>?> GetAll()
	{
		var result = await httpClient.GetAsync("api/employees");
		result.EnsureSuccessStatusCode();
		var employees = await result.Content.ReadFromJsonAsync<IEnumerable<Employee>>();
		return employees;
	}

	public async Task<Employee?> Get(int id)
	{
		var request = new HttpRequestMessage(HttpMethod.Get, $"api/employees/{id}");
		var response = await SendRequestAsync(request);
		return await response.Content.ReadFromJsonAsync<Employee>();
	}

	public async Task<Employee?> InsertOrUpdate(Employee employee)
	{
		var request = new HttpRequestMessage(HttpMethod.Post, "api/employees/insertOrUpdate")
		{
			Content = JsonContent.Create(employee)
		};
		var response = await SendRequestAsync(request);
		return await response.Content.ReadFromJsonAsync<Employee>();
	}

	public async Task Remove(Employee employee)
	{
		var request = new HttpRequestMessage(HttpMethod.Delete, "api/employees/delete")
		{
			Content = JsonContent.Create(employee)
		};
		await SendRequestAsync(request);
	}

	private async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage request)
	{
		var token = await sessionStorage.GetItemAsStringAsync("accessToken");
		var authHeader = new AuthenticationHeaderValue("Bearer", token);
		request.Headers.Authorization = authHeader;
		var respond = await httpClient.SendAsync(request);
		respond.EnsureSuccessStatusCode();
		return respond;
	}
}
