using ShiftPlan.Blazor.WebAssembly.Exceptions;
using ShiftPlan.Blazor.WebAssembly.Clients;
using ShiftPlan.Blazor.WebAssembly.Models;

namespace ShiftPlan.Blazor.WebAssembly.Services;

public interface IShiftsService
{
	Task<IEnumerable<Shift>> GetAll();
	Task<Shift> GetShift(int id);
	Task<Shift> InsertOrUpdate(Shift shift);
	Task Remove(Shift shift);
}

public class ShiftsService(IShiftsClient client) : IShiftsService
{
	public async Task<IEnumerable<Shift>> GetAll() => await client.GetAll() ?? [];

	public async Task<Shift> GetShift(int id) => await client.Get(id) ?? throw new NotFoundException("Shift has not been found");

	public async Task<Shift> InsertOrUpdate(Shift shift) => await client.InsertOrUpdate(shift);

	public async Task Remove(Shift shift)
	{
		await client.Remove(shift);
	}
}

