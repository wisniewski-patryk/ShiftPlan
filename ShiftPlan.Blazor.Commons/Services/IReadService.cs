
namespace ShiftPlan.Blazor.Commons.Services
{
    public interface IReadService<T>
    {
        IEnumerable<T> DeserializeFileAsList(string file);
        T DeserializeFileSingle(string file);
        void SerializeFileSingle(T o, string file);
        void SerializeFileAsList(IList<T> o, string fileName);
    }
}