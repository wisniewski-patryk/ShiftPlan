using FrontendBlazor.Client.Clients;
using FrontendBlazor.Client.Models;

namespace FrontendBlazor.Client.Services;

public interface IShiftsService
{
    Task<IEnumerable<Shift>> GetAll();
    Task<Shift?> GetShift(int id);
    Task InsertOrUpdate(Shift shift);
    Task Remove(Shift shift);
}

public class ShiftsService(IShiftsClient client) : IShiftsService
{
    public async Task<IEnumerable<Shift>> GetAll()
    {
        return await client.GetAll() ?? [];
    }

    public async Task<Shift?> GetShift(int id) => await client.Get(id);

    public async Task InsertOrUpdate(Shift shift) => await client.InsertOrUpdate(shift);

    public async Task SaveWork(Shift[] shifts) => await client.SaveWork(shifts);

    public async Task Remove(Shift shift) => await client.Remove(shift);
}

