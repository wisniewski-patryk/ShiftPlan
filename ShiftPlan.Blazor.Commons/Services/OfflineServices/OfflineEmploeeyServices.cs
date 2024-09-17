using ShiftPlan.Blazor.Commons.Exceptions;
using ShiftPlan.Blazor.Commons.Models;

namespace ShiftPlan.Blazor.Commons.Services.OfflineServices;

public class OfflineEmploeeyServices(List<Employee> employees) : IEmployeesService
{
	public Task<IEnumerable<Employee>> GetAll()
	{
		return Task.FromResult(employees.AsEnumerable());
	}

	public Task<Employee> GetEmployee(int id)
	{
		return Task.FromResult(employees.Find(e => e.Id == id) ?? throw new NotFoundException("Employee has been not found"));

	}

	public Task<Employee> InsertOrUpdate(Employee employ)
	{
		var exists = employees.Find(e => employ == e);
		if (exists is not null)
			employees.Remove(exists);
		employ = employ with { Id = (int)DateTime.UtcNow.Ticks };
		employees.Add(employ);
		return Task.FromResult(employ);
	}

	public Task Remove(Employee employ)
	{
		employees.Remove(employ);
		return Task.CompletedTask;
	}
}
