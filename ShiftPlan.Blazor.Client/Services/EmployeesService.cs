using ShiftPlan.Blazor.Client.Clients;
using ShiftPlan.Blazor.Client.Models;

namespace ShiftPlan.Blazor.Client.Services;

public interface IEmployeesService
{
	Task<IEnumerable<Employee>?> GetAll();
	Task<Employee?> GetEmployee(int id);
	Task<Employee?> InsertOrUpdate(Employee employ);
	Task<bool> Remove(Employee employ);
}

public class EmployeesService(IEmployeesClient client) : IEmployeesService
{
	public async Task<IEnumerable<Employee>?> GetAll() => await client.GetAll();

	public async Task<Employee?> GetEmployee(int id) => await client.Get(id);

	public async Task<Employee?> InsertOrUpdate(Employee employ) => await client.InsertOrUpdate(employ);

	public async Task<bool> Remove(Employee employ)
	{
		try
		{
			await client.Remove(employ);
			return true;
		}
		catch
		{
			return false;
		}
	}
}

