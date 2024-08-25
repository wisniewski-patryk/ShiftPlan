using ShiftPlan.Blazor.Commons.Clients;
using ShiftPlan.Blazor.Commons.Models;

namespace ShiftPlan.Blazor.Commons.Services;

public interface IEmployeesService
{
	Task<IEnumerable<Employee>?> GetAll();
	Task<Employee?> GetEmployee(int id);
	Task<Employee?> InsertOrUpdate(Employee employ);
	Task Remove(Employee employ);
}

public class EmployeesService(IEmployeesClient client) : IEmployeesService
{
	public async Task<IEnumerable<Employee>?> GetAll() => await client.GetAll();

	public async Task<Employee?> GetEmployee(int id) => await client.Get(id);

	public async Task<Employee?> InsertOrUpdate(Employee employ) => await client.InsertOrUpdate(employ);

	public async Task Remove(Employee employ)
	{
		await client.Remove(employ);
	}
}

