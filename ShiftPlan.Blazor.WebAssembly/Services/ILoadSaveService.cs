using ShiftPlan.Blazor.Commons.Models;

namespace ShiftPlan.Blazor.Commons.Services;

public interface ILoadSaveService
{
	Task SaveLocalFile(LocalShiftsAndEmployees o, string file);
	Task<LocalShiftsAndEmployees> LoadLocalFile(string file);
}
