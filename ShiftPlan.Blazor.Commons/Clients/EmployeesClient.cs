using Blazored.SessionStorage;
using ShiftPlan.Blazor.Commons.Models;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ShiftPlan.Blazor.Commons.Clients;

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
		var respons = await httpClient.GetAsync("api/employees");
		respons.EnsureSuccessStatusCode();
		return await respons.Content.ReadFromJsonAsync<IEnumerable<Employee>>();
	}

	public async Task<Employee?> Get(int id)
	{
		var request = new HttpRequestMessage(HttpMethod.Get, $"api/employees/{id}");
		var response = await SendRequestAsync(request);
		return response.StatusCode != HttpStatusCode.OK ?
			null :
			await response.Content.ReadFromJsonAsync<Employee>();
	}

	public async Task<Employee?> InsertOrUpdate(Employee employee)
	{
		var request = new HttpRequestMessage(HttpMethod.Post, "api/employees/insertOrUpdate")
		{
			Content = JsonContent.Create(employee)
		};
		var response = await SendRequestAsync(request);
		
		return response.StatusCode != HttpStatusCode.OK ?
			null :
			await response.Content.ReadFromJsonAsync<Employee>();
	}

	public async Task Remove(Employee employee)
	{
		var request = new HttpRequestMessage(HttpMethod.Delete, "api/employees/delete")
		{
			Content = JsonContent.Create(employee)
		};
		var response = await SendRequestAsync(request);
		response.EnsureSuccessStatusCode();
	}

	private async Task<HttpResponseMessage?> SendRequestAsync(HttpRequestMessage request)
	{
		var token = await sessionStorage.GetItemAsStringAsync("accessToken");
		request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
		HttpResponseMessage? response = null;
		try
		{
			response = await httpClient.SendAsync(request);
			response.EnsureSuccessStatusCode();
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
		return response;
	}
}
