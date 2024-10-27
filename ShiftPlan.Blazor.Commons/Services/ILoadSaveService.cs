using ShiftPlan.Blazor.Commons.Models;

namespace ShiftPlan.Blazor.Commons.Services
{
	public interface ILoadSaveService
	{
		Task<IEnumerable<Shift>> LoadFileAsList(string file);
		Task<Shift> LoadFileAsSingle(string file);
		Task SaveFileAsSingle(Shift o, string file);
		Task SaveFileAsList(IList<Shift> o, string file);
		Task SaveLocalFileAsSingle(LocalDataObject o, string file);
		Task<LocalDataObject> LoadLocalFileAsSingle(string file);
	}
}
