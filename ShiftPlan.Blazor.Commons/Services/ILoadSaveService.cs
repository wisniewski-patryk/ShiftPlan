namespace ShiftPlan.Blazor.Commons.Services
{
	public interface ILoadSaveService<T>
	{
		Task<IEnumerable<T>> LoadFileAsList(string file);
		Task<T> LoadFileAsSingle(string file);
		Task SaveFileAsSingle(T o, string file);
		Task SaveFileAsList(IList<T> o, string file);
	}
}
