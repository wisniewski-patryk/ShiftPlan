using ShiftPlan.Blazor.Commons.Services;

namespace ShiftPlan.Blazor.Services;

public class LocalFileService<T> : ILoadSaveService<T>
{
	public Task<IEnumerable<T>> LoadFileAsList(string file)
	{
		throw new NotSupportedException("Supported only in client side application");
	}

	public Task<T> LoadFileAsSingle(string file)
	{
		throw new NotSupportedException("Supported only in client side application");
	}

	public Task SaveFileAsList(IList<T> o, string file)
	{
		throw new NotSupportedException("Supported only in client side application");
	}

	public Task SaveFileAsSingle(T o, string file)
	{
		throw new NotSupportedException("Supported only in client side application");
	}
}
