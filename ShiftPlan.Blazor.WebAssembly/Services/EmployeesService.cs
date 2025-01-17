﻿using ShiftPlan.Blazor.WebAssembly.Exceptions;
using ShiftPlan.Blazor.WebAssembly.Clients;
using ShiftPlan.Blazor.WebAssembly.Models;

namespace ShiftPlan.Blazor.WebAssembly.Services;

public interface IEmployeesService
{
	Task<IEnumerable<Employee>> GetAll();
	Task<Employee> GetEmployee(int id);
	Task<Employee> InsertOrUpdate(Employee employ);
	Task Remove(Employee employ);
}

public class EmployeesService(IEmployeesClient client) : IEmployeesService
{
	public async Task<IEnumerable<Employee>> GetAll() => await client.GetAll() ?? [];

	public async Task<Employee> GetEmployee(int id) => await client.Get(id) ?? throw new NotFoundException("Employee has been not found");

	public async Task<Employee> InsertOrUpdate(Employee employ) => await client.InsertOrUpdate(employ);

	public async Task Remove(Employee employ) => await client.Remove(employ);
}

