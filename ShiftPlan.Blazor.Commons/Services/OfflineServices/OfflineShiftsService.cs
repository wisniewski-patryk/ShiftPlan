using ShiftPlan.Blazor.Commons.Exceptions;
using ShiftPlan.Blazor.Commons.Models;

namespace ShiftPlan.Blazor.Commons.Services.OfflineServices;

public class OfflineShiftsService(List<Shift> shifts) : IShiftsService
{
	public Task<IEnumerable<Shift>> GetAll()
	{
		return Task.FromResult(shifts.AsEnumerable());
	}

	public Task<Shift> GetShift(int id)
	{
		return Task.FromResult(shifts.FirstOrDefault(s => s.Id == id) ?? throw new NotFoundException("Shift has not been found"));
	}

	public Task<Shift> InsertOrUpdate(Shift shift)
	{
		var exists = shifts.Find(s => s == shift);
		if (exists is not null)
		{
			shifts.Remove(exists);
		}
		shift = shift with { Id = (int)DateTime.UtcNow.Ticks };
		shifts.Add(shift);

		return Task.FromResult(shift);
	}

	public Task Remove(Shift shift)
	{
		shifts.Remove(shift);
		return Task.CompletedTask;
	}
}
