using ShiftPlan.Blazor.Commons.Services;
using System.Text.Json;

namespace ShiftPlan.Blazor.Client.Services;

public class LocalFileService<T> : ILoadSaveService<T>
{
	public async Task<IEnumerable<T>> LoadFileAsList(string file)
	{
		if (File.Exists(file))
		{	
			using FileStream result = File.OpenRead(file);
			if (result.Length == 0)
				throw new JsonException("Json file is empty");
			try
			{
				return await JsonSerializer.DeserializeAsync<IEnumerable<T>>(result);
			}
			catch { throw new InvalidDataException("Json file is not correct"); }
		}
            throw new FileNotFoundException("Json file does not exists");
        }

	public async Task<T> LoadFileAsSingle(string file)
	{
		if (File.Exists(file))
		{
			using FileStream result = File.OpenRead(file);
			if (result.Length == 0)
				throw new JsonException("Json file is empty");
			try
			{
				return await JsonSerializer.DeserializeAsync<T>(result);
			}
			catch { throw new InvalidDataException("Json file is not correct"); }
		}
            throw new FileNotFoundException("Json file does not exists");
        }

	public async Task SaveFileAsList(IList<T> o, string file)
	{
		try
		{
			using FileStream createStream = File.Create(file);
			await JsonSerializer.SerializeAsync(createStream, o);
		} catch { throw new FileNotFoundException("Json file does not created"); } 
    }

	public async Task SaveFileAsSingle(T o, string file)
	{
		try
		{
			using FileStream createStream = File.Create(file);
			await JsonSerializer.SerializeAsync(createStream, o);
		}
        catch { throw new FileNotFoundException("Json file does not created"); }
    }
}
