using ShiftPlan.Blazor.Commons.Services;
using System.Text.Json;

namespace ShiftPlan.Blazor.Client.Services
{
	public class LocalFileService<T> : ILoadSaveService<T>
	{
		public async Task<IEnumerable<T>> LoadFileAsList(string file)
		{
			if (File.Exists(file))
			{
				using var reader = new StreamReader(file);
				var result = await reader.ReadToEndAsync();
				return JsonSerializer.Deserialize<IEnumerable<T>>(result);
			}
			throw new Exception("json file is not correct!");
		}

		public async Task<T> LoadFileAsSingle(string file)
		{
			if (File.Exists(file))
			{
				using var reader = new StreamReader(file);
				var result = await reader.ReadToEndAsync();
				return JsonSerializer.Deserialize<T>(result);
			}
			throw new Exception("json file is not correct!");
		}

		public async Task SaveFileAsList(IList<T> o, string file)
		{
			await using FileStream createStream = File.Create(file);
			await JsonSerializer.SerializeAsync(createStream, o);
		}

		public async Task SaveFileAsSingle(T o, string file)
		{
			await using FileStream createStream = File.Create(file);
			await JsonSerializer.SerializeAsync(createStream, o);
		}
	}
}
