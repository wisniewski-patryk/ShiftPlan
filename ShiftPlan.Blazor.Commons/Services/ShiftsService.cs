﻿using ShiftPlan.Blazor.Commons.Clients;
using ShiftPlan.Blazor.Commons.Models;

namespace ShiftPlan.Blazor.Commons.Services;

public interface IShiftsService
{
	Task<IEnumerable<Shift>?> GetAll();
	Task<Shift?> GetShift(int id);
	Task<Shift?> InsertOrUpdate(Shift shift);
	Task Remove(Shift shift);
}

public class ShiftsService(IShiftsClient client) : IShiftsService
{
	public async Task<IEnumerable<Shift>?> GetAll() => await client.GetAll();

	public async Task<Shift?> GetShift(int id) => await client.Get(id);

	public async Task<Shift?> InsertOrUpdate(Shift shift) => await client.InsertOrUpdate(shift);

	public async Task Remove(Shift shift)
	{
		await client.Remove(shift);
	}
}
