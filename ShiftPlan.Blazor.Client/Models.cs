namespace ShiftPlan.Blazor.Client.Models;

public record Employee(string Name, int? Id = null);

public record Shift(Employee Employee, DateOnly Date, TimeOnly Start, TimeOnly End, State State = State.Neutral, int? Id = null);

public enum ShiftType
{
	First,
	Second,
	Third
}

public enum State
{
	Neutral,
	Added,
	Modified,
	Deleted,
}