using ShiftPlan.Blazor.Commons.Models;
using ShiftPlan.Blazor.Commons.Services;

namespace ShiftPlan.Blazor.Services;

public class LocalFileService : ILoadSaveService
{
	public Task<IEnumerable<Shift>> LoadFileAsList(string file)
	{
		throw new NotSupportedException("Supported only in client side application");
	}

	public Task<Shift> LoadFileAsSingle(string file)
	{
		throw new NotSupportedException("Supported only in client side application");
	}

	public Task<LocalDataObject> LoadLocalFileAsSingle(string file)
	{
		throw new NotImplementedException();
	}

	public Task SaveFileAsList(IList<Shift> o, string file)
	{
		throw new NotSupportedException("Supported only in client side application");
	}

	public Task SaveFileAsSingle(Shift o, string file)
	{
		throw new NotSupportedException("Supported only in client side application");
	}

	public Task SaveLocalFileAsSingle(LocalDataObject o, string file)
	{
		throw new NotImplementedException();
	}
}
