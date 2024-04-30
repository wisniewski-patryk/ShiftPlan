using FrontendBlazor.Client.Clients;
using FrontendBlazor.Client.Models;

namespace FrontendBlazor.Client.Services;

public interface IEmployeesService
{
	Task<IEnumerable<Employee>> GetAll();
	Task<Employee?> GetEmployee(int id);
	Task InsertOrUpdate(Employee employ);
	Task Remove(Employee employ);
}

public class EmployeesService(IEmployeesClient client) : IEmployeesService
{
	public async Task<IEnumerable<Employee>> GetAll()
	{
		return await client.GetAll() ?? [];
	}

	public async Task<Employee?> GetEmployee(int id) => await client.Get(id);

	public async Task InsertOrUpdate(Employee employ) => await client.InsertOrUpdate(employ);

	public async Task Remove(Employee employ) => await client.Remove(employ);
}

