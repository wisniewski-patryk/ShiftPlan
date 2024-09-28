
namespace ShiftPlan.Blazor.Commons.Services
{
    public interface ILoadSaveService<T>
    {
        IEnumerable<T> LoadFileAsList(string file);
        T LoadFileAsSingle(string file);
        void SaveFileAsSingle(T o, string file);
        void SaveFileAsList(IList<T> o, string fileName);
    }
}