using Newtonsoft.Json;

namespace ShiftPlan.Blazor.Commons.Services;

public interface ILoadSaveService<T>
{
	IEnumerable<T> LoadFileAsList(string file);
	T LoadFileAsSingle(string file);
	void SaveFileAsSingle(T o, string file);
	void SaveFileAsList(IList<T> o, string fileName);
}
public class JsonService<T> : ILoadSaveService<T>
{
        public IEnumerable<T> LoadFileAsList(string file)
        {
            if (File.Exists(file))
            {
                string jsonString = File.ReadAllText(file);
                IList<T> result = JsonConvert.DeserializeObject<List<T>>(jsonString);
                return result;
            }
            throw new Exception("json file is not correct!");
        }

        public T LoadFileAsSingle(string file)
        {
            if (File.Exists(file))
            {
                string jsonString = File.ReadAllText(file);
                T result = JsonConvert.DeserializeObject<T>(jsonString);
                return result;
            }
            throw new Exception("json file is not correct!");
        }

        public void SaveFileAsSingle(T o, string fileName)
        {
            using (var sw = new StreamWriter(fileName))
            {
                new JsonSerializer().Serialize(sw, o);
            }
        }

        public void SaveFileAsList(IList<T> o, string fileName)
        {
            using (var sw = new StreamWriter(fileName))
            {
                new JsonSerializer().Serialize(sw, o);
            }
        }
}

