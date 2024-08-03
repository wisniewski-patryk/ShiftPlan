using Blazored.SessionStorage;
using ShiftPlan.Blazor.Client.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ShiftPlan.Blazor.Client.Clients;

public interface IShiftsClient
{
	Task<Shift?> Get(int id);
	Task<IEnumerable<Shift>?> GetAll();
	Task<Shift?> InsertOrUpdate(Shift shift);
	Task Remove(Shift shift);
}

public class ShiftsClient(HttpClient httpClient, ISessionStorageService sessionStorage) : IShiftsClient
{
	public async Task<IEnumerable<Shift>?> GetAll()
	{
		var shifts = await httpClient.GetFromJsonAsync<IEnumerable<Shift>>("api/shifts");
		return shifts;
	}

	public async Task<Shift?> Get(int id)
	{
		var request = new HttpRequestMessage(HttpMethod.Get, $"api/shifts/{id}");
		var response = await SendRequestAsync(request);
		return await response.Content.ReadFromJsonAsync<Shift>();
	}

	public async Task<Shift?> InsertOrUpdate(Shift shift)
	{
		var request = new HttpRequestMessage(HttpMethod.Post, "api/shifts/insertOrUpdate")
		{
			Content = JsonContent.Create(shift)
		};
		var response = await SendRequestAsync(request);
		response.EnsureSuccessStatusCode();
		return await response.Content.ReadFromJsonAsync<Shift>();
	}

	public async Task Remove(Shift shift)
	{
		var request = new HttpRequestMessage(HttpMethod.Delete, "api/shifts/delete")
		{
			Content = JsonContent.Create(shift)
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

// IDEAS TODO: Implement responses for requests, example - InsertOrUpdate return InsertOrUpdateResponse(Status.Inserted/Status.Updated, shift?) etc. 
