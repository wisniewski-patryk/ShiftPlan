namespace ShiftPlan.Blazor.WebAssembly.Models;

public record Shift(Employee Employee, DateOnly Date, TimeOnly Start, TimeOnly End, int? Id = null);
