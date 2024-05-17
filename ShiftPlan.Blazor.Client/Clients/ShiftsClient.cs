using ShiftPlan.Blazor.Client.Models;
using System.Net.Http.Json;

namespace ShiftPlan.Blazor.Client.Clients;

public interface IShiftsClient
{
	Task<Shift?> Get(int id);
	Task<IEnumerable<Shift>?> GetAll();
	Task<Shift?> InsertOrUpdate(Shift shift);
	Task Remove(Shift shift);
}

public class ShiftsClient(HttpClient httpClient) : IShiftsClient
{
	public async Task<IEnumerable<Shift>?> GetAll()
	{
		var shifts = await httpClient.GetFromJsonAsync<IEnumerable<Shift>>("shifts");
		return shifts;
	}

	public async Task<Shift?> Get(int id)
	{
		return await httpClient.GetFromJsonAsync<Shift>($"shifts/{id}");
	}

	public async Task<Shift?> InsertOrUpdate(Shift shift)
	{
		var response = await httpClient.PostAsJsonAsync("shifts/insertOrUpdate", shift);
		response.EnsureSuccessStatusCode();
		return await response.Content.ReadFromJsonAsync<Shift>();
	}

	public async Task Remove(Shift shift)
	{
		var request = new HttpRequestMessage(HttpMethod.Delete, "shifts/delete")
		{
			Content = JsonContent.Create(shift)
		};
		var response = await httpClient.SendAsync(request);
		response.EnsureSuccessStatusCode();
	}
}

// IDEAS TODO: Implement responses for requests, example - InsertOrUpdate return InsertOrUpdateResponse(Status.Inserted/Status.Updated, shift?) etc. 
