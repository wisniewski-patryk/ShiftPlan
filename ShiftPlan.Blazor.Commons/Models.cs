namespace ShiftPlan.Blazor.Commons.Models;

public record Employee(string Name, int? Id = null);

public record Shift(Employee Employee, DateOnly Date, TimeOnly Start, TimeOnly End, int? Id = null);

public record LocalDataObject(List<Employee>? Employees, List<Shift>? Shifts);

public enum ShiftType
{
	First,
	Second,
	Third,
	Other
}
