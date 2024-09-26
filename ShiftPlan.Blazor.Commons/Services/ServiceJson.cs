using Newtonsoft.Json;

namespace ShiftPlan.Blazor.Commons.Services
{
    public class ServiceJson<T> : IReadService<T>
    {
        public IEnumerable<T> DeserializeFileAsList(string file)
        {
            if (File.Exists(file))
            {
                string jsonString = File.ReadAllText(file);
                IList<T> result = JsonConvert.DeserializeObject<List<T>>(jsonString);
                return result;
            }
            return default;
        }

        public T DeserializeFileSingle(string file)
        {
            if (File.Exists(file))
            {
                string jsonString = File.ReadAllText(file);
                T result = JsonConvert.DeserializeObject<T>(jsonString);
                return result;
            }
            return default;
        }

        public void SerializeFileSingle(T o, string fileName)
        {
            using (var sw = new StreamWriter(fileName))
            {
                new JsonSerializer().Serialize(sw, o);
            }
        }

        public void SerializeFileAsList(IList<T> o, string fileName)
        {
            using (var sw = new StreamWriter(fileName))
            {
                new JsonSerializer().Serialize(sw, o);
            }
        }
    }
}
