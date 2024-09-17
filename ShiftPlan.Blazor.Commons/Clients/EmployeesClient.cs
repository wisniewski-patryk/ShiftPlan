using ShiftPlan.Blazor.Commons.Exceptions;
using ShiftPlan.Blazor.Commons.Models;
using System.Net.Http.Json;

namespace ShiftPlan.Blazor.Commons.Clients;

public interface IEmployeesClient
{
	Task<Employee> Get(int id);
	Task<IEnumerable<Employee>> GetAll();
	Task<Employee> InsertOrUpdate(Employee employee);
	Task Remove(Employee employee);
}

public class EmployeesClient(HttpClient httpClient) : IEmployeesClient
{
	public async Task<IEnumerable<Employee>> GetAll()
	{
		var respons = await httpClient.GetAsync("api/employees");

		if (respons.IsSuccessStatusCode)
			return await respons.Content.ReadFromJsonAsync<IEnumerable<Employee>>() ?? [];

		throw new HttpCommunicationException(respons.ReasonPhrase ?? "Communication error", respons.StatusCode);

	}

	public async Task<Employee> Get(int id)
	{
		var request = new HttpRequestMessage(HttpMethod.Get, $"api/employees/{id}");
		var response = await httpClient.SendAsync(request);

		if (response.IsSuccessStatusCode)
			return await response.Content.ReadFromJsonAsync<Employee>() ?? throw new Exception("Serialization exception");

		throw new HttpCommunicationException(response.ReasonPhrase ?? "Communication error", response.StatusCode);
	}

	public async Task<Employee> InsertOrUpdate(Employee employee)
	{
		var request = new HttpRequestMessage(HttpMethod.Post, "api/employees/insertOrUpdate")
		{
			Content = JsonContent.Create(employee)
		};
		var response = await httpClient.SendAsync(request);
		if (response.IsSuccessStatusCode)
			return await response.Content.ReadFromJsonAsync<Employee>() ?? throw new Exception("Serialization exception");

		throw new HttpCommunicationException(response.ReasonPhrase ?? "Communication error", response.StatusCode);
	}

	public async Task Remove(Employee employee)
	{
		var request = new HttpRequestMessage(HttpMethod.Delete, "api/employees/delete")
		{
			Content = JsonContent.Create(employee)
		};
		var response = await httpClient.SendAsync(request);
		if (!response.IsSuccessStatusCode)
			throw new HttpCommunicationException(response.ReasonPhrase ?? "Communication error", response.StatusCode);
	}
}
