using ShiftPlan.Blazor.Commons.Models;
using ShiftPlan.Blazor.Commons.Services;
using System.Text.Json;

namespace ShiftPlan.Blazor.Client.Services;

public class LocalFileService : ILoadSaveService
{
	public async Task<IEnumerable<Shift>> LoadFileAsList(string file)
	{
		if (File.Exists(file))
		{	
			using FileStream result = File.OpenRead(file);
			if (result.Length == 0)
				throw new JsonException("Json file is empty");
			try
			{
				return await JsonSerializer.DeserializeAsync<IEnumerable<Shift>>(result);
			} catch { throw new InvalidDataException("Json file is not correct"); }
		}
        throw new FileNotFoundException("Json file does not exists");
    }

	public async Task<Shift> LoadFileAsSingle(string file)
	{
		if (File.Exists(file))
		{
			using FileStream result = File.OpenRead(file);
			if (result.Length == 0)
				throw new JsonException("Json file is empty");
			try
			{
				return await JsonSerializer.DeserializeAsync<Shift>(result);
			} catch { throw new InvalidDataException("Json file is not correct"); }
		}
        throw new FileNotFoundException("Json file does not exists");
    }

	public async Task SaveFileAsList(IList<Shift> o, string file)
	{
		try
		{
			using FileStream createStream = File.Create(file);
			await JsonSerializer.SerializeAsync(createStream, o);
		} catch { throw new FileNotFoundException("Json file does not created"); } 
    }

	public async Task SaveFileAsSingle(Shift o, string file)
	{
		try
		{
			using FileStream createStream = File.Create(file);
			await JsonSerializer.SerializeAsync(createStream, o);
		} catch { throw new FileNotFoundException("Json file does not created"); }
    }

	public async Task<LocalDataObject> LoadLocalFileAsSingle(string file)
	{
		if (File.Exists(file))
		{
			using FileStream result = File.OpenRead(file);
			if (result.Length == 0)
				throw new JsonException("Json file is empty");
			try
			{
				return await JsonSerializer.DeserializeAsync<LocalDataObject>(result);
			}
			catch { throw new InvalidDataException("Json file is not correct"); }
		}
		throw new FileNotFoundException("Json file does not exists");
	}

	public async Task SaveLocalFileAsSingle(LocalDataObject o, string file)
	{
		try
		{
			using FileStream createStream = File.Create(file);
			await JsonSerializer.SerializeAsync(createStream, o);
		}
		catch { throw new FileNotFoundException("Json file does not created"); }
	}
}
