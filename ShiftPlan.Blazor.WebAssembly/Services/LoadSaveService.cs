using ShiftPlan.Blazor.WebAssembly.Models;
using System.Text.Json;

namespace ShiftPlan.Blazor.WebAssembly.Services;

public interface ILoadSaveService
{
	Task SaveLocalFile(LocalShiftsAndEmployees data, string file);
	Task<LocalShiftsAndEmployees> LoadLocalFile(string file);
}

public class LocalFileService : ILoadSaveService
{
	public async Task<LocalShiftsAndEmployees> LoadLocalFile(string file)
	{
		if (File.Exists(file))
		{
			using FileStream result = File.OpenRead(file);
			if (result.Length == 0)
				throw new JsonException("Json file is empty");
			try
			{
				return await JsonSerializer.DeserializeAsync<LocalShiftsAndEmployees>(result);
			}
			catch { throw new InvalidDataException("Json file is not correct"); }
		}
		throw new FileNotFoundException("Json file does not exists");
	}

	public async Task SaveLocalFile(LocalShiftsAndEmployees o, string file)
	{
		try
		{
			using FileStream createStream = File.Create(file);
			await JsonSerializer.SerializeAsync(createStream, o);
		}
		catch { throw new FileNotFoundException("Json file does not created"); }
	}
}
