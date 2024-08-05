using ShiftPlan.Blazor.Client.Clients;
using ShiftPlan.Blazor.Client.Models;

namespace ShiftPlan.Blazor.Client.Services;

public interface IShiftsService
{
	Task<IEnumerable<Shift>?> GetAll();
	Task<Shift?> GetShift(int id);
	Task<Shift?> InsertOrUpdate(Shift shift);
	Task<bool> Remove(Shift shift);
}

public class ShiftsService(IShiftsClient client) : IShiftsService
{
	public async Task<IEnumerable<Shift>?> GetAll() => await client.GetAll();

	public async Task<Shift?> GetShift(int id) => await client.Get(id);

	public async Task<Shift?> InsertOrUpdate(Shift shift) => await client.InsertOrUpdate(shift);

	public async Task<bool> Remove(Shift shift)
	{
		try
		{
			await client.Remove(shift);
			return true;
		}
		catch
		{
			return false;
		}
	}
}

