namespace ShiftPlan.Api.Models;

public class Shift : BaseEntity
{
	public Employee? Employee { get; set; }
	public DateOnly Date { get; set; }
	public TimeOnly Start { get; set; }
	public TimeOnly End { get; set; }
}
