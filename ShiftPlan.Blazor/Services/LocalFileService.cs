using ShiftPlan.Blazor.Commons.Models;
using ShiftPlan.Blazor.Commons.Services;

namespace ShiftPlan.Blazor.Services;

public class LocalFileService : ILoadSaveService
{
	public Task<LocalShiftsAndEmployees> LoadLocalFile(string file)
	{
		throw new NotImplementedException();
	}

	public Task SaveLocalFile(LocalShiftsAndEmployees o, string file)
	{
		throw new NotImplementedException();
	}
}
