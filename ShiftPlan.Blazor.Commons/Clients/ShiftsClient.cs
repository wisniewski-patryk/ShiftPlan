using ShiftPlan.Blazor.Commons.Exceptions;
using ShiftPlan.Blazor.Commons.Models;
using System.Net.Http.Json;

namespace ShiftPlan.Blazor.Commons.Clients;

public interface IShiftsClient
{
	Task<Shift> Get(int id);
	Task<IEnumerable<Shift>> GetAll();
	Task<Shift> InsertOrUpdate(Shift shift);
	Task Remove(Shift shift);
}

public class ShiftsClient(HttpClient httpClient) : IShiftsClient
{
	public async Task<IEnumerable<Shift>> GetAll()
	{
		var respond = await httpClient.GetAsync("api/shifts");
		if (respond.IsSuccessStatusCode)
			return await respond.Content.ReadFromJsonAsync<IEnumerable<Shift>>() ?? [];

		throw new HttpCommunicationException(respond.ReasonPhrase ?? "Communication error", respond.StatusCode);
	}

	public async Task<Shift> Get(int id)
	{
		var request = new HttpRequestMessage(HttpMethod.Get, $"api/shifts/{id}");
		var response = await httpClient.SendAsync(request);

		if (response.IsSuccessStatusCode)
			return await response.Content.ReadFromJsonAsync<Shift>() ?? throw new Exception("Serialization exception");

		throw new HttpCommunicationException(response.ReasonPhrase ?? "Communication error", response.StatusCode);
	}

	public async Task<Shift> InsertOrUpdate(Shift shift)
	{
		var request = new HttpRequestMessage(HttpMethod.Post, "api/shifts/insertOrUpdate")
		{
			Content = JsonContent.Create(shift)
		};
		var response = await httpClient.SendAsync(request);
		if (response.IsSuccessStatusCode)
			return await response.Content.ReadFromJsonAsync<Shift>() ?? throw new Exception("Serialization exception");

		throw new HttpCommunicationException(response.ReasonPhrase ?? "Communication error", response.StatusCode);
	}

	public async Task Remove(Shift shift)
	{
		var request = new HttpRequestMessage(HttpMethod.Delete, "api/shifts/delete")
		{
			Content = JsonContent.Create(shift)
		};
		var response = await httpClient.SendAsync(request);
		if (!response.IsSuccessStatusCode)
			throw new HttpCommunicationException(response.ReasonPhrase ?? "Communication error", response.StatusCode);
	}
}